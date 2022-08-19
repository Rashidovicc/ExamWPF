using examWPF.Data.IRepository;
using examWPF.Domain.Entities;

namespace examWPF.Data.Repository
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        
    }
}