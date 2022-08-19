using examWPF.Domain.Entities;

namespace examWPF.Service.DTOs.UserDTO
{
    public class UserForCreation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }

    }
}