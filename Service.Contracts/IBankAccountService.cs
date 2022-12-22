using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBankAccountService
    {
        Task<bool> CreateRecord(BankAccountCreateUpdateDto createBankAccountDto, int userId);
        Task<bool> UpdateRecord(BankAccountCreateUpdateDto updateBankAccountDto, int id, int userId);
        Task<bool> DeleteRecord(int id);
    }
}
