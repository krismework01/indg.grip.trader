using AutoMapper;
using INDG.GRIP.Trader.Application.Common.Mappings;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using System;

namespace INDG.GRIP.Trader.Application.Logic.Products.Models
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price));
        }
    }
}
