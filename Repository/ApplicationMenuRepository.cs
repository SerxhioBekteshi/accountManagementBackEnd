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
    public class ApplicationMenuRepository : RepositoryBase<ApplicationMenu>, IApplicationMenuRepository
    {
        public ApplicationMenuRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ApplicationMenu>> GetMenu(bool trackChanges, int RoleId) => 
            FindByCondition( c => c.RoleId.Equals(RoleId),trackChanges).ToList();
    }
}
