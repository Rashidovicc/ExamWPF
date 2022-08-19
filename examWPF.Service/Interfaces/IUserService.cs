using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using examWPF.Domain.Entities;
using examWPF.Service.DTOs.UserDTO;

namespace examWPF.Service.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> GetAsync(long id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> CreateAsync(UserForCreation User);

        Task<User> UpdateAsync(long id, UserForCreation User);

        Task<bool> DeleteAsync(long id);

        Task UploadPicturesAsync(long id, string imagePath, string passportPath);
    }
}