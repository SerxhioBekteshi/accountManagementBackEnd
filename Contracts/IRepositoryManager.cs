using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        ICurrencyRepository Currency { get; }
        IBankRepository Bank{ get; }
        IBankTransactionRepository BankTransaction{ get; }
        ISaleTransactionRepository SalesTransaction { get; }
        IProductSaleListRepository ProductSaleList { get; }
        IApplicationMenuRepository ApplicationMenu { get; }
        IUserRepository User { get; }
        ICompanyCategoryRepository CompanyCategory { get; }
        ICompanyProductRepository CompanyProduct { get; }
        ICurrencyBankAccountRepository CurrencyBank { get; }
        IBankAccountRepository BankAccount { get; }

        Task SaveAsync();
        void Save();
    }
}