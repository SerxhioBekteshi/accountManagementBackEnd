using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICurrencyBankService
    {
        Task<bool> CreateRecord(CurrencyBankCreateUpdateDto createCompanyCategoryDto, int userId);
        Task<bool> UpdateRecord(CurrencyBankCreateUpdateDto updateCompanyCategoryDto, int id, int userId);
        Task<bool> DeleteRecord(int id);
    }
}
