using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBankService
    {
        Task<PagedListResponse<IEnumerable<BankDto>>> GetAllRecords(RequestTableBodyDto request);
        Task<int> CreateRecord(BankForCreationAndUpdateDto createBankAccountDTO, int userId);
        Task<bool> UpdateRecord(int id, BankForCreationAndUpdateDto updateBankAccountDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<BankDto> GetRecordById(int id);
        Task<IEnumerable<BankListDto>> GetBanksAsAList();
        Task<IEnumerable<BankDto>> GetAllBanksPerLoggedUser(int userId);

    }
}
