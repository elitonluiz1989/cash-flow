using AutoMapper;
using CashFlow.Desktop.ViewModels.Products;
using CashFlow.Entities;
using CashFlow.Shared;

namespace CashFlow.Desktop
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ConstructUsing(d => ServiceProviderAcessor.GetRequiredService<ProductViewModel>())
                .ForMember(d => d.CancelCommand, o => o.Ignore())
                .ForMember(d => d.ResetCommand, o => o.Ignore())
                .ForMember(d => d.SaveCommand, o => o.Ignore())
                .ReverseMap();
        }
    }
}
