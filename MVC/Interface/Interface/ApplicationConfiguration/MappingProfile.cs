using AutoMapper;
using Entities.Models;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;

namespace Interface.ApplicationConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();
            CreateMap<TaxpayerProfileVM, ApplicationUser>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.BranchAddress.Country))
                .ForMember(dest => dest.Governate, opt => opt.MapFrom(src => src.BranchAddress.Governate))
                .ForMember(dest => dest.RegionCity, opt => opt.MapFrom(src => src.BranchAddress.RegionCity))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.BranchAddress.Street))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BranchAddress.BuildingNumber));

            CreateMap<ApplicationUser, TaxpayerProfileVM>()
                .ForPath(dest=>dest.BranchAddress , src=>src.MapFrom(src=> new BranchAddressVM(){
                    BuildingNumber = src.BuildingNumber,
                    Country = src.Country,
                    Street = src.Street,
                    Governate = src.Governate,
                    RegionCity = src.RegionCity
                }));

            CreateMap<ReceiptVM, Receipt>()
                .ForMember(dest => dest.Header, src => src.MapFrom(src => new Header()
                {
                    DateTimeIssued = src.DateTime.AddHours(-2),
                    ReceiptNumber = src.ReceiptNumber,
                    Uuid = "",
                    Currency = "EGP",
                    ReferenceOldUUID = "",
                    PreviousUUID = "",
                    ExchangeRate = 0,
                    OrderdeliveryMode = "",
                    SOrderNameCode = ""
                }))
                .ForMember(dest=>dest.DocumentType , src=>src.MapFrom(src => new DocumentType()
                {
                    ReceiptType = src.Type,
                    TypeVersion = src.Version
                }))
                .ForMember(dest => dest.Buyer, src => src.MapFrom(src => new Buyer()
                {
                    Name = src.IssuedName,
                    Type = src.IssuedType,
                    Id = src.IssuedNumber,
                    MobileNumber = src.IssuedPhone,
                    PaymentNumber = null,
                }));
            CreateMap<ItemVM, Item>();
            CreateMap<ApplicationUser, Receipt>()
                .ForMember(dest=>dest.ApplicationUserId,src=>src.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Seller , src=>src.MapFrom(src=> new Seller()
                {
                    Rin = src.RegistrationNumber,
                    ActivityCode = src.ActivityCodes,
                    CompanyTradeName = src.Name,
                    DeviceSerialNumber = src.POSSerial,
                    BranchAddress = new BranchAddress()
                    {
                        BuildingNumber = src.BuildingNumber,
                        Country = src.Country,
                        RegionCity = src.RegionCity,
                        Governate = src.Governate,
                        Street = src.Street
                    },
                    BranchCode = src.BranchCode,
                }));
            
            
            


        }
    }
}
