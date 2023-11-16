using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
                CreateMap<Product,ProductToReturnDto>()
                .ForMember(PD=>PD.ProducrBrand, O=>O.MapFrom(P=>P.ProductBrand.Name))
                .ForMember(PD => PD.ProducrType, O => O.MapFrom(P => P.ProductType.Name))
                .ForMember(PD=>PD.ProducrBrandId, O=>O.MapFrom(P=>P.ProductBrand.Id))
                .ForMember(PD => PD.ProducrTypeId, O => O.MapFrom(P => P.ProductType.Id))
                .ForMember(PD=>PD.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDTO>().ReverseMap();

            CreateMap<AddressDTO,Talabat.Core.Entities.Order_Aggregation.Address>();

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>().ReverseMap();


        }



    }
}
