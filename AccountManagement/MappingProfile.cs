using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace AccountManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(" ", x.Address, x.Country)));
            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryListDto>().ReverseMap();


            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Currency, CurrencyDto>().ReverseMap();

            CreateMap<BankAccount, BankAccountDto>().ReverseMap();

            CreateMap<BankTransaction, BankTransactionDto>().ReverseMap();

            CreateMap<SaleTransaction, SaleTransactionDto>().ReverseMap();

            CreateMap<ProductSaleList, ProductSaleListDto>().ReverseMap();

            CreateMap<Product, ProductSaleListDto>().ReverseMap();

            CreateMap<ApplicationMenu, MenuDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Company, PostCategoriesToCompanyDto>().ReverseMap();


            CreateMap<CompanyForCreationAndUpdateDto, Company>().ReverseMap();

            CreateMap<EmployeeForCreationAndUpdateDto, Employee>().ReverseMap();

            CreateMap<ProductForCreationAndUpdateDto, Product>().ReverseMap();

            CreateMap<CurrencyCreateDto, Currency>().ReverseMap();

            CreateMap<CategoryForCreationAndUpdateDto, Category>().ReverseMap();

            CreateMap<BankAccountForCreationAndUpdateDto, BankAccount>().ReverseMap();

            CreateMap<BankTransactionForCreateDto, BankTransaction>().ReverseMap();

            CreateMap<ProductSaleListForCreationDto, ProductSaleList>().ReverseMap();

            CreateMap<SaleTransactionForCreationDto, SaleTransaction>().ReverseMap();

            CreateMap<UserForUpdateDto, User>().ReverseMap();

            CreateMap<ResetPasswordDto, User>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>().ReverseMap();

            CreateMap<UserForAuthenticationDto, User>().ReverseMap();
        }
    }
}
