using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDtoManagerList>> GetAllManagersAsync(AutocompleteDto autocomplete);
    }
}
