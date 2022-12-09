using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async  Task<User> GetUserDetails(bool trackChanges, string userId) => 
            FindByCondition(c => c.Id == Int16.Parse(userId), trackChanges).SingleOrDefault();

        public void UpdateUserDetails(User user) => Update(user);

    }
}
