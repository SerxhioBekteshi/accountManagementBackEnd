using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IEmployeeService EmployeeService { get; }
        ICompanyService CompanyService { get; }
        ICategoryService CategoryService { get; }
        ICompanyCategoryService CompanyCategoryService { get; }
        IProductService ProductService { get; }
        IBankAccountService BankAccountService { get; }
        IUserService UserService { get; }
        ICurrencyService CurrencyService { get; }
        ICurrencyBankService CurrencyBankService { get; }
    }
}
