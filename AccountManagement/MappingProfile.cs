using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Company
            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<CompanyForCreationAndUpdateDto, Company>().ReverseMap();

            CreateMap<Company, PostCategoriesToCompanyDto>().ReverseMap();


            //Employee
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<EmployeeForCreationAndUpdateDto, Employee>().ReverseMap();


            //Category
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryListDto>().ReverseMap();

            CreateMap<CategoryForCreationAndUpdateDto, Category>().ReverseMap();


            //Product
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Product, ProductSaleListDto>().ReverseMap();

            CreateMap<ProductForCreationAndUpdateDto, Product>().ReverseMap();


            //user
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserForUpdateDto, User>().ReverseMap();

            CreateMap<ResetPasswordDto, User>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>().ReverseMap();

            CreateMap<UserForAuthenticationDto, User>().ReverseMap();


            //Currency
            CreateMap<Currency, CurrencyDto>().ReverseMap();

            CreateMap<CurrencyForCreationAndUpdateDto, Currency>().ReverseMap();

            CreateMap<Currency, PostBanksToCurrencyDto>().ReverseMap();


            //Bank
            CreateMap<Bank, BankDto>().ReverseMap();

            CreateMap<BankForCreationAndUpdateDto, Bank>().ReverseMap();


            //BankTransaction
            CreateMap<BankTransaction, BankTransactionDto>().ReverseMap();

            CreateMap<BankTransactionForCreateDto, BankTransaction>().ReverseMap();


            //SaleTransaction
            CreateMap<SaleTransaction, SaleTransactionDto>().ReverseMap();

            CreateMap<SaleTransactionForCreationDto, SaleTransaction>().ReverseMap();

            CreateMap<ProductSaleList, ProductSaleListDto>().ReverseMap();

            CreateMap<ProductSaleListForCreationDto, ProductSaleList>().ReverseMap();


            //Menu
            CreateMap<ApplicationMenu, MenuDto>().ReverseMap();


            //BankAccount
            CreateMap<BankAccount, BankAccountDto>().ReverseMap();

        }
    }
}
