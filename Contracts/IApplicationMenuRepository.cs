using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IApplicationMenuRepository
    {
        Task<IEnumerable<ApplicationMenu>> GetMenu(bool trackChanges, int RoleId);
    }
}
