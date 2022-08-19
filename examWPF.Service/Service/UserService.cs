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
        private readonly AttachmentRepository attachmentRepository;


        private readonly string _url = ApiConstants.BASE_URL + "Students/";

        public UserService()
        {
            attachmentRepository = new AttachmentRepository();
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
                ("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:12345")));

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
            var newUser = JsonConvert.SerializeObject(student);

            var requestContent = new StringContent
                (newUser, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync
                (_url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                var createdUser = JsonConvert.DeserializeObject<User>(content);

                await _userRepository.CreateAsync(createdUser);

                await _userRepository.SaveAsync();

                return createdUser;
            }

            return null;
        }

        public async Task<User> UpdateAsync(long id, UserForCreation student)
        {
            var newUserAsJson = JsonConvert.SerializeObject(student);

            StringContent responseContent = new(newUserAsJson,
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync
                (_url + id, responseContent);


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                content = content.Replace('\\', '/');

                var updatedUser = JsonConvert.DeserializeObject<User>(content);

                await _userRepository.UpdateAsync(updatedUser);

                await _userRepository.SaveAsync();
                return updatedUser;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync(_url + id);

            if (response.IsSuccessStatusCode)
            {
                if (id > 320)
                {
                    await _userRepository.DeleteAsync(p => p.Id == id);
                   await _userRepository.SaveAsync();
                }
                return true;

            }
            return false;
        }

        public async Task UploadPicturesAsync(long id, string imagePath, string passportPath)
        {
            using HttpClient client = new();

            MultipartFormDataContent formData = new();

            if (imagePath is not null)
            {
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");
            }
            if (passportPath is not null)
            {
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");
            }

            var isUploadedSucceccfully = await client.PostAsync(_url + "attachments/" + id, formData);



            if (isUploadedSucceccfully.IsSuccessStatusCode)
            {
                var response = await GetAsync(id);

                await attachmentRepository.CreateAsync(response.Image);

                await attachmentRepository.CreateAsync(response.Passport);

               await attachmentRepository.SaveAsync();
                
               return;
            }
            
            

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}

