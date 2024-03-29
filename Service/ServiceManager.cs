﻿using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager: IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeesService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICompanyCategoryService> _companyCategoryService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBankService> _bankService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ICurrencyService> _currencyService;
        private readonly Lazy<ICurrencyBankService> _currencyBankService;
        private readonly Lazy<IBankAccountService> _bankAccountService;

        public ServiceManager(IRepositoryManager repositoryManager
        , IDapperRepository dapperRepository
        , ILoggerManager logger
        , IMapper mapper
 
        )
        {
            _employeesService = new Lazy<IEmployeeService>(() => new EmployeeService(logger, mapper, repositoryManager, dapperRepository));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(logger, mapper, repositoryManager, dapperRepository));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(logger, mapper, repositoryManager, dapperRepository));
            _companyCategoryService = new Lazy<ICompanyCategoryService>(() => new CompanyCategoryService(logger, mapper, repositoryManager, dapperRepository));
            _productService = new Lazy<IProductService>(() => new ProductService(logger, mapper, repositoryManager, dapperRepository));
            _bankService = new Lazy<IBankService>(() => new BankService(logger, mapper, repositoryManager, dapperRepository));
            _userService = new Lazy<IUserService>(() => new UserService(logger, mapper, repositoryManager, dapperRepository));
            _currencyService = new Lazy<ICurrencyService>(() => new CurrencyService(logger, mapper, repositoryManager, dapperRepository));
            _currencyBankService = new Lazy<ICurrencyBankService>(() => new CurrencyBankService(logger, mapper, repositoryManager, dapperRepository));
            _bankAccountService = new Lazy<IBankAccountService>(() => new BankAccountService(logger, mapper, repositoryManager, dapperRepository));

        }
        public IEmployeeService EmployeeService => _employeesService.Value;
        public ICompanyService CompanyService => _companyService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public ICompanyCategoryService CompanyCategoryService => _companyCategoryService.Value;
        public IProductService ProductService => _productService.Value;
        public IBankService BankService => _bankService.Value;
        public IUserService UserService => _userService.Value;
        public ICurrencyService CurrencyService => _currencyService.Value;
        public ICurrencyBankService CurrencyBankService => _currencyBankService.Value;
        public IBankAccountService BankAccountService => _bankAccountService.Value;

    }
}
