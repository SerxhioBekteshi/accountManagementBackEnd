using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private ICurrencyRepository _currencyRepository;
        private IBankAccountRepository _bankAccountRepository;
        private IBankTransactionRepository _bankTransactionRepository;
        private ISaleTransactionRepository _salesTransactionRepository;
        private IProductSaleListRepository _productSaleListRepository;
        private IApplicationMenuRepository _applicationMenuRepository;
        private IUserRepository _userRepository;
        private ICompanyCategoryRepository _companyCategoryRepository;
        private ICompanyProductRepository _companyProductRepository;
        private ICurrencyBankAccountRepository _currencyBankRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);

                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_repositoryContext);

                return _categoryRepository;
            }
        }
        public ICurrencyRepository Currency
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_repositoryContext);

                return _currencyRepository;
            }
        }
        
        public IBankAccountRepository BankAccount
        {
            get
            {
                if (_bankAccountRepository == null)
                    _bankAccountRepository = new BankAccountRepository(_repositoryContext);

                return _bankAccountRepository;
            }
        }

        public IBankTransactionRepository BankTransaction
        {
            get
            {
                if (_bankTransactionRepository == null)
                    _bankTransactionRepository = new BankTransactionRepository(_repositoryContext);

                return _bankTransactionRepository;
            }
        }

        public ISaleTransactionRepository SalesTransaction
        {
            get
            {
                if (_salesTransactionRepository == null)
                    _salesTransactionRepository = new SaleTransactionRepository(_repositoryContext);

                return _salesTransactionRepository;
            }
        }

        public IProductSaleListRepository ProductSaleList
        {
            get
            {
                if (_productSaleListRepository == null)
                    _productSaleListRepository = new ProductSaleListRepository(_repositoryContext);

                return _productSaleListRepository;
            }
        }

        public IApplicationMenuRepository ApplicationMenu
        {
            get
            {
                if (_applicationMenuRepository == null)
                    _applicationMenuRepository = new ApplicationMenuRepository(_repositoryContext);

                return _applicationMenuRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public ICompanyCategoryRepository CompanyCategory
        {
            get
            {
                if (_companyCategoryRepository == null)
                    _companyCategoryRepository = new CompanyCategoryRepository(_repositoryContext);

                return _companyCategoryRepository;
            }
        }

        public ICompanyProductRepository CompanyProduct
        {
            get
            {
                if (_companyProductRepository == null)
                    _companyProductRepository = new CompanyProductRepository(_repositoryContext);

                return _companyProductRepository;
            }
        }

        public ICurrencyBankAccountRepository CurrencyBank
        {
            get
            {
                if (_currencyBankRepository == null)
                    _currencyBankRepository = new CurrencyBankRepository(_repositoryContext);

                return _currencyBankRepository;
            }
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
        public void Save() => _repositoryContext.SaveChanges();

    }
}