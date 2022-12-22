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
    public interface ICurrencyService
    {
        Task<PagedListResponse<IEnumerable<CurrencyDto>>> GetAllRecords(RequestTableBodyDto request);
        Task<int> CreateRecord(CurrencyForCreationAndUpdateDto createCompanyDTO, int userId); 
        Task<bool> UpdateRecord(int id, CurrencyForCreationAndUpdateDto updateCompanyDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<CurrencyDto> GetRecordById(int id);
        Task<IEnumerable<BankDto>> GetBanksForCurrency(int currencyId);
        Task<bool> PostBanksToCurrency(int currencyId, PostBanksToCurrencyDto postBanksToCurrency, int userId);
        Task<bool> DeleteRelationCurrency(int bankId, int currencyId);
    }
}
