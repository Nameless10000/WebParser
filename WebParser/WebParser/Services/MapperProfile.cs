using AutoMapper;

namespace WebParser.Services
{
    public class ParserMapperProfile : Profile
    {
        public ParserMapperProfile()
        {
            CreateMap<ShopAddDto, Shop>();
            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, BestPriceProduct>()
                .ForMember(p => p.BestPrice, opt =>
                    opt.MapFrom(p => p.LastFetchedPrice))
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name.Name));
        }
    }
}
