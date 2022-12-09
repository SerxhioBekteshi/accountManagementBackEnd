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
        Task<int> CreateRecord(CurrencyCreateDto createCompanyDTO, int userId); 
        Task<bool> UpdateRecord(int id, CurrencyCreateDto updateCompanyDto, int userId);
        Task<bool> DeleteRecord(int id);
        Task<CompanyDto> GetRecordById(int id);
        Task<IEnumerable<CategoryDto>> GetCurrenciesForBankAccount(int bankAccountId);
        Task<bool> PostCurrenciesForBank(int bankId, PostCurrenciesToBankDto postCurrenciesToBankId);
        Task<bool> DeleteRelationCurrency(int bankId, int currencyId);
    }
}
