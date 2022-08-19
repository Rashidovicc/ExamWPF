using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using examWPF.Data.IRepository;
using examWPF.Data.Repository;
using examWPF.Domain.Entities;
using examWPF.Service.Constants;
using examWPF.Service.DTOs.UserDTO;
using examWPF.Service.Interfaces;
using Newtonsoft.Json;

namespace examWPF.Service.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserRepository _userRepository;

        private readonly string _url = ApiConstants.BASE_URL + "Students/";
        public UserService()
        {
            _userRepository = new UserRepository();
            _httpClient = new HttpClient();
        }

        public async Task<User> GetAsync(long id)
        {
            var response = await _httpClient.GetAsync(_url + id);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<User>(resultContent);
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Basic",Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:12345")));
            
            var response = await _httpClient.GetAsync(_url);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            }
            return null;
        }

        public async Task<User> CreateAsync(UserForCreation student)
        {
            var Student = JsonConvert.SerializeObject(student);
            var content = new StringContent(Student, System.Text.Encoding.UTF8, "application/json");
            var response =await _httpClient.PostAsync(_url, content);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                
                var res2 = JsonConvert.DeserializeObject<User>(result);
                await _userRepository.CreateAsync(res2);
                await _userRepository.SaveAsync();

                return res2;
            }

            return null;
        }

        public async Task<User> UpdateAsync(long id, UserForCreation student)
        {
            var newstudentAsJson = JsonConvert.SerializeObject(student);

            StringContent responseContent = new StringContent(newstudentAsJson, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync(_url + id, responseContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var res2 = JsonConvert.DeserializeObject<User>(content);
                await _userRepository.UpdateAsync(res2);
                await _userRepository.SaveAsync();
                
                return res2;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response =await _httpClient.DeleteAsync(_url + id);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task PassPicturesAsync(long id, string imagePath, string passportPath)
        {
            using HttpClient client = new HttpClient();
            
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (imagePath is not null)
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");

            if (passportPath is not null)
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");

            HttpResponseMessage response = await client.PostAsync(_url + "attachments/" + id, formData);

            string message = await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}