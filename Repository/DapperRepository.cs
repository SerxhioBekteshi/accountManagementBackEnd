using Contracts;
using Dapper;
using Entities;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.Extensions.Logging;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _context;

        public DapperRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);

                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(Guid id)
        {
            var query = "SELECT * FROM Companies WHERE CompanyId = @Id";

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyForCreationAndUpdateDto company)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
            "SELECT CAST(SCOPE_IDENTITY() AS int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
                return createdCompany;
            }
        }

        private static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public async Task UpdateCompany(Guid id, CompanyForCreationAndUpdateDto company)
        {
            var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE CompanyId = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCompany(Guid id)
        {
            var query = "DELETE FROM Companies WHERE CompanyId = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


        public async Task<Company> GetCompanyByEmployeeId(Guid id)
        {
            var procedureName = "ShowCompanyByEmployeeId";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return company;
            }
        }

        public async Task<Company> GetMultipleResults(Guid id)
        {
            var query = "SELECT * FROM Companies WHERE CompanyId = @Id;" +
                "SELECT * FROM Employees WHERE CompanyId = @Id";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Company>();
                if (company is not null)
                    company.Employees = (await multi.ReadAsync<Employee>()).ToList();

                return company;
            }
        }

        public async Task<IEnumerable<Company>> MultipleMapping()
        {
            var query = "SELECT * FROM Companies c JOIN Employees e ON c.CompanyId = e.CompanyId";
            var companyDict = new Dictionary<int, Company>();

            using (var connection = _context.CreateConnection())
            {
                IEnumerable<Company> companies = await connection.QueryAsync<Company, Employee, Company>(query, (company, employess) =>
                {
                    Company companyy;
                    if (!companyDict.TryGetValue(company.Id, out company))
                    {
                        companyy = company;
                        company.Employees = new List<Employee>();
                        companyDict.Add(company.Id, company);
                    }

                    if (employess.Id.CompareTo(0) > 0)
                        company.Employees.Add(employess);

                    return company;
                }, splitOn: "CompanyId");

                var cc = companies.Distinct().ToList();

                return cc;
                //var result  = await connection.QueryAsync<Company>(query);
                //return result;
                //var companies = await connection.QueryAsync<Company, Employee, Company>(
                //    query, (company, employee) =>
                //    {
                //        if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                //        {
                //            currentCompany = company;
                //            companyDict.Add(currentCompany.Id, currentCompany);
                //        }

                //        currentCompany.Employees.Add(employee);

                //        return currentCompany;
                //    }, 
                //    splitOn: "CompanyId");

                //return companies.Distinct().ToList();
            }
        }

        public async Task<IEnumerable<BankTransaction>> GetBankTransactionsBasedOnBankAccount(int id)
        {
            var query = "SELECT * FROM BankTransactions Where BankAccountId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var bankTransactions = await connection.QueryAsync<BankTransaction>(query, new { id });

                return bankTransactions.OrderBy(c => c.DateCreated).ToList();
            }
        }

        public async Task<IEnumerable<BankAccount>> GetActiveAccounts(string userId)
        {
            var query = "SELECT * FROM BankAccounts Where ClientId = @userId And isActive = 1";
            using (var connection = _context.CreateConnection())
            {
                var bankAccounts = await connection.QueryAsync<BankAccount>(query, new { userId });

                return bankAccounts.ToList();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsPerCategory(int id)
        {
            var query = "SELECT * FROM Products Where CategoryId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var bankAccounts = await connection.QueryAsync<Product>(query, new { id });

                return bankAccounts.ToList();
            }
        }

        public async Task<IEnumerable<BankAccount>> GetGeneralInfo()
        {
            var query = "SELECT * FROM Currencies INNER JOIN " +
                "(AspNetUsers users INNER JOIN BankAccounts banks ON banks.ClientId LIKE users.Id) ON Currencies.Id = banks.CurrencyId ";

            using (var connection = _context.CreateConnection())
            {
                //var res = await connection.QueryAsync<BankAccount, User, Currency, BankAccount>(query, (bankAccount, user, currency) =>
                //{

                //    bankAccount.CurrencyId = currency.Id;
                //    bankAccount.ClientId = new Guid(user.Id);
                //    return bankAccount;
                //}, splitOn: "CurrencyId, ClientId");
                //return res;
                var res = await connection.QueryAsync<BankAccount>(query);
                return res;


            }


        }

        public async Task<IEnumerable<SaleTransactionDto>> GetSalesAndRespectiveProducts()
        {
            var query = @"SELECT * 
                FROM SalesTransaction as sales INNER JOIN ProductSaleList as productList
                ON productList.SaleTransactionId = sales.Id";

            var SalesDictionary = new Dictionary<int, SaleTransactionDto>();

            using (var connection = _context.CreateConnection())
            {

                IEnumerable<SaleTransactionDto> result = await connection.QueryAsync<SaleTransactionDto, ProductSaleListDto, SaleTransactionDto>(query, (sales, productList) =>
                {
                    SaleTransactionDto salesTransaction;
                    if (!SalesDictionary.TryGetValue(sales.Id, out salesTransaction))
                    {
                        salesTransaction = sales;
                        salesTransaction.ProductSale = new List<ProductSaleListDto>();
                        sales.ProductSale.Add(productList);
                        SalesDictionary.Add(sales.Id, salesTransaction);
                    }

                    //if (productList.Id > 0)

                    return salesTransaction;
                }, splitOn: "SaleTransactionId");

                var salesTransaction = result.Distinct().ToList();

                return salesTransaction;
            }
        }





        ////////ME SERVICE MANAGER

        public async Task<IEnumerable<EmployeeDto>> EmployeesTable(string userId, int companyId, RequestTableBodyDto request)
        {
            var query = @"SELECT Employees.Id, Employees.Name, Employees.Age, Employees.Position FROM Employees LEFT JOIN Companies 
                        ON Employees.CompanyId = Companies.Id WHERE Companies.ManagerId = @userId AND Companies.Id = @companyId ";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<EmployeeDto>(query, new { userId, companyId });
                var searchTerm = request.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Position.ToLower().Contains(searchTerm)
                        || e.Age.Equals(searchTerm)
                    );

                }
                return result;
            }
        }

        public async Task<IEnumerable<CategoryDto>> CategoriesTable( RequestTableBodyDto request)
        {
            var query = @"SELECT Categories.Id, Categories.Code, Categories.Description FROM Categories";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CategoryDto>(query);
                var searchTerm = request.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.Code.ToLower().Contains(searchTerm) || e.Description.ToLower().Contains(searchTerm));

                }
                return result;
            }
        }



        public async Task<IEnumerable<ProductDto>> ProductTable( RequestTableBodyDto request)
        {
            var query = @"SELECT *  FROM Products ";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<ProductDto>(query);
                //var searchTerm = request.SearchTerm;

                //if (searchTerm is not null)
                //{
                //    searchTerm = searchTerm.Trim().ToLower();
                //    result = result.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Position.ToLower().Contains(searchTerm)
                //        || e.Age.Equals(searchTerm)
                //    );

                //}
                return result;
            }
        }

        public async Task<IEnumerable<CompanyDto>> CompanyTable(RequestTableBodyDto request)
        {
            var query = @"
               SELECT Companies.Id, Companies.Name, Companies.Address, Companies.Country, Companies.ManagerId,Companies.ManagerAccountActivated, AspNetUsers.FirstName, AspNetUsers.LastName 
                FROM Companies LEFT JOIN AspNetUsers ON Companies.ManagerId = AspNetUsers.Id
             ";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CompanyDto>(query);
                var searchTerm = request.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Address.ToLower().Contains(searchTerm)
                        || e.Country.ToLower().Contains(searchTerm)
                    );

                }
                return result;

            }
        }

        public async Task<IEnumerable<CompanyDto>> CompanyTablePerLoggedInManager(int userId, RequestTableBodyDto request)
        {
            var query = @"
               SELECT Companies.Id, Companies.Name, Companies.Address, Companies.Country, Companies.ManagerId, AspNetUsers.FirstName, AspNetUsers.LastName 
                FROM Companies LEFT JOIN AspNetUsers ON Companies.ManagerId = AspNetUsers.Id WHERE Companies.ManagerId = @userId
             ";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CompanyDto>(query, new { userId });
                var searchTerm = request.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Address.ToLower().Contains(searchTerm)
                        || e.Country.ToLower().Contains(searchTerm)
                    );

                }
                return result;

            }
        }

        public async Task<PagedList<EmployeeDto>> SearchEmployees(string userId, int companyId, RequestTableBodyDto request)
        {
            var result = await EmployeesTable(userId, companyId, request);
            return PagedList<EmployeeDto>.ToPagedList(result, request.PageNumber, request.PageSize);
        }

         public async Task<PagedList<CategoryDto>> SearchCategories(RequestTableBodyDto request)
        {
            var result = await CategoriesTable(request);
            return PagedList<CategoryDto>.ToPagedList(result, request.PageNumber, request.PageSize);
        }


        public async Task<PagedList<CompanyDto>> SearchCompanies(RequestTableBodyDto request)
        {
            var result = await CompanyTable(request);
            return PagedList<CompanyDto>.ToPagedList(result, request.PageNumber, request.PageSize);
        }

        public async Task<PagedList<ProductDto>> SearchProducts(RequestTableBodyDto request)
        {
            var result = await ProductTable(request);
            return PagedList<ProductDto>.ToPagedList(result, request.PageNumber, request.PageSize);
        }

        public async Task<PagedList<CompanyDto>> SearchCompaniesByLoggedInManager(int userId, RequestTableBodyDto request)
        {
            var result = await CompanyTablePerLoggedInManager(userId,request);
            return PagedList<CompanyDto>.ToPagedList(result, request.PageNumber, request.PageSize);
        }

        public async Task<IEnumerable<CategoryListDto>> GetCategoriesAsList()
        {
            var query = @"SELECT 
		            c.Id AS value
		            , c.Code AS label	        
	            FROM Categories c";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CategoryListDto>(query);
       
                return result;

            }
        }



        public async Task<IEnumerable<BankAccountDto>> GetBankAccountsForLoggedUser(int userId, RequestTableBodyDto request)
        {
            var query = @"
               SELECT BankAccounts.Id, BankAccounts.Name, BankAccounts.Code,BankAccounts.Balance, BankAccounts.isActive, BankAccounts.ClientId, AspNetUsers.FirstName, AspNetUsers.LastName 
                FROM BankAccounts INNER JOIN AspNetUsers ON BankAccounts.ClientId = AspNetUsers.Id WHERE BankAccounts.ClientId = @userId
             ";

            string lookupSortNormalized = OrderQueryBuilder.NormalizeLookUpOrderBy(request.Sorting);
            if (!string.IsNullOrWhiteSpace(lookupSortNormalized))
                query += $" ORDER BY {lookupSortNormalized}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<BankAccountDto>(query, new { userId });
                var searchTerm = request.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.Name.ToLower().Contains(searchTerm) || e.Code.ToLower().Contains(searchTerm)
                        || e.Balance.ToString().Contains(searchTerm)
                    );

                }
                return result;

            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesForCompanyId(int companyId)
        {
            var query = @"
               SELECT Categories.Id, Categories.Description, Categories.Code, Categories.Image
                FROM Categories INNER JOIN CompanyCategory ON CompanyCategory.CategoryId = Categories.Id WHERE CompanyCategory.companyId = @companyId
             ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CategoryDto>(query, new { companyId });

                return result;

            }
        }

        public async Task<IEnumerable<BankAccountDto>> GetBanksForCurrencyId(int currencyId)
        {
            var query = @"
               SELECT Currencies.Id, Currencies.Description, Currencies.Code, Currencies.ExchangeRate
                FROM Currencies INNER JOIN CurrencyBank ON CurrencyBank.BankId = Currencies.Id WHERE CompanyCategory.currencyId = @currencyId
             ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<BankAccountDto>(query, new { currencyId });

                return result;

            }
        }

        public async Task<IEnumerable<UserDtoManagerList>> GetAllManagers(AutocompleteDto autocomplete)
        {
            var top = autocomplete.Top;
            var query = @"
               SELECT  TOP (@top) AspNetUsers.Id AS Value, CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) AS Label
                FROM AspNetUsers
                INNER JOIN AspNetUserRoles ON AspNetUserRoles.UserId = AspNetUsers.Id 
                INNER JOIN AspNetRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId 
                WHERE AspNetRoles.Id = 1 
         
             ";


            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<UserDtoManagerList>(query, new { top });
                var searchTerm = autocomplete.SearchTerm;

                if (searchTerm is not null)
                {
                    searchTerm = searchTerm.Trim().ToLower();
                    result = result.Where(e => e.label.ToLower().Contains(searchTerm));

                }

                return result;

            }
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var query = @"
            SELECT *
            FROM (
	            SELECT 
		            c.Id
		            , c.Code
		            , c.Description
		            , c.DateCreated
		            , c.CreatedBy
		            , CONCAT(uc.FirstName, ' ', uc.LastName) AS CreatedByFullName
		            , c.DateModified
		            , c.ModifiedBy
		            , CONCAT(um.FirstName, '', um.LastName) AS ModifiedByFullName
	            FROM Categories c 
		            LEFT JOIN AspNetUsers uc ON c.CreatedBy = uc.Id
		            LEFT JOIN AspNetUsers um ON c.ModifiedBy = um.Id
                    WHERE c.Id = @categoryId
                ) result
            ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CategoryDto>(query, new { categoryId });
                return result;
            }
        }

       

        public async Task<CompanyDto> GetCompanyById(int companyId)
        {
            var query = @"
            SELECT *
            FROM (
             SELECT 
              c.Id
              , c.Name
              , c.Address
              , c.Country
              , c.ManagerAccountActivated
              , c.DateCreated
              , c.CreatedBy
              , CONCAT(uc.FirstName, ' ', uc.LastName) AS CreatedByFullName
              , c.DateModified
              , c.ModifiedBy
              , CONCAT(um.FirstName, '', um.LastName) AS ModifiedByFullName
             FROM Companies c 
              LEFT JOIN AspNetUsers uc ON c.CreatedBy = uc.Id
              LEFT JOIN AspNetUsers um ON c.ModifiedBy = um.Id
                    WHERE c.Id = @companyId
                ) result
            ";

            //var query = @"SELECT * FROM Companies c WHERE c.Id = @companyId ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CompanyDto>(query, new { companyId });
                return result;
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(int employeeId)
        {
            var query = @"
            SELECT *
            FROM (
	            SELECT 
		            e.Id
		            , e.Name
		            , e.Age
                    , e.Position
		            , e.CompanyId 
		            
	            FROM Employees e
		          
                    WHERE e.Id = @employeeId
                ) result
            ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<EmployeeDto>(query, new { employeeId });
                return result;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var query = @"SELECT * FROM Products p WHERE p.Id = @productId";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<ProductDto>(query, new { productId });
                return result;
            }
        }

        public async Task<BankAccountDto> GetBankAccountById(int bankAccountId)
        {
            var query = @"SELECT * FROM BankAccounts b WHERE b.Id = @bankAccountId";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<BankAccountDto>(query, new { bankAccountId });
                return result;
            }
        }

        public async Task<CurrencyDto> GetCurrencyById(int currencyId)
        {
            var query = @"
            SELECT *
            FROM (
             SELECT 
              c.Id
              , c.Code
              , c.Description
              , c.ExchangeRate
              , c.DateCreated
              , c.CreatedBy
              , CONCAT(uc.FirstName, ' ', uc.LastName) AS CreatedByFullName
              , c.DateModified
              , c.ModifiedBy
              , CONCAT(um.FirstName, '', um.LastName) AS ModifiedByFullName
             FROM Currencies c 
              LEFT JOIN AspNetUsers uc ON c.CreatedBy = uc.Id
              LEFT JOIN AspNetUsers um ON c.ModifiedBy = um.Id
                    WHERE c.Id = @currencyId
                ) result
            ";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CurrencyDto>(query, new { currencyId });
                return result;
            }
        }

    }
}