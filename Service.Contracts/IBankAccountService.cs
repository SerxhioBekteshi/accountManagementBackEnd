using Entities.DTO;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccountDto>> GetAllRecords(int userId, RequestTableBodyDto request);
        Task<int> CreateRecord(BankAccountForCreationAndUpdateDto createBankAccountDTO, int userId);
        Task<bool> UpdateRecord(int id, BankAccountForCreationAndUpdateDto updateBankAccountDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<BankAccountDto> GetRecordById(int id);
    }
}
