using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDapperRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> CreateCompany(CompanyForCreationAndUpdateDto company);
        public Task UpdateCompany(Guid id, CompanyForCreationAndUpdateDto company);
        public Task DeleteCompany(Guid id);
        public Task<Company> GetCompany(Guid id);
        public Task<Company> GetCompanyByEmployeeId(Guid id);
        public Task<Company> GetMultipleResults(Guid id);
        public Task<IEnumerable<Company>> MultipleMapping();
        public Task<IEnumerable<BankTransaction>> GetBankTransactionsBasedOnBankAccount(int id);
        public Task<IEnumerable<BankAccount>> GetActiveAccounts(string userId);
        public Task<IEnumerable<Product>> GetProductsPerCategory(int id);
        public Task<IEnumerable<BankAccount>> GetGeneralInfo();
        public Task<IEnumerable<SaleTransactionDto>> GetSalesAndRespectiveProducts();

        //SERVICE
        public Task<PagedList<EmployeeDto>> SearchEmployees(string userId, int companyId, RequestTableBodyDto request);
        public Task<PagedList<ProductDto>> SearchProducts(RequestTableBodyDto request);
        public Task<PagedList<CompanyDto>> SearchCompaniesByLoggedInManager(int userId, RequestTableBodyDto request);
        public Task<PagedList<CategoryDto>> SearchCategories(RequestTableBodyDto request);
        public Task<PagedList<CompanyDto>> SearchCompanies(RequestTableBodyDto request);
        public Task<PagedList<CurrencyDto>> SearchCurrencies(RequestTableBodyDto request);
        public Task<PagedList<BankDto>> SearchBanks(RequestTableBodyDto request);
        public Task<IEnumerable<EmployeeDto>> EmployeesTable(string userId, int companyId, RequestTableBodyDto request);
        public Task<IEnumerable<CompanyDto>> CompanyTable(RequestTableBodyDto request);
        public Task<IEnumerable<CategoryDto>> CategoriesTable(RequestTableBodyDto request);
        public Task<IEnumerable<ProductDto>> ProductTable(RequestTableBodyDto request);
        public Task<IEnumerable<CurrencyDto>> CurrencyTable(RequestTableBodyDto request);
        public Task<IEnumerable<CompanyDto>> CompanyTablePerLoggedInManager(int userId, RequestTableBodyDto request);
        public Task<IEnumerable<BankDto>> BanksTable(RequestTableBodyDto request);


        public Task<CompanyDto> GetCompanyById(int companyId);
        public Task<CategoryDto> GetCategoryById(int categoryId);
        public Task<EmployeeDto> GetEmployeeById(int employeeId);
        public Task<ProductDto> GetProductById(int productId);
        public Task<BankDto> GetBankById(int bankAccountId);
        public Task<CurrencyDto> GetCurrencyById(int currencyId);
        public Task<IEnumerable<CategoryDto>> GetCategoriesForCompanyId(int companyId);
        public Task<IEnumerable<Entities.DTO.BankDto>> GetBanksForCurrencyId(int currencyId);
        public Task<IEnumerable<CategoryListDto>> GetCategoriesAsList();
        public Task<IEnumerable<BankListDto>> GetBanksAsList();
        public Task<IEnumerable<BankDto>> GetBanksPerUser(int userId);
        public Task<IEnumerable<UserDtoManagerList>> GetAllManagers(AutocompleteDto autocomplete);


        Task<bool> DeleteBankCurrency(int?[] bankIds, int currencyId);

    }
}
