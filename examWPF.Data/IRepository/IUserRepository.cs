using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using examWPF.Domain.Entities;

namespace examWPF.Data.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
      
    }
}