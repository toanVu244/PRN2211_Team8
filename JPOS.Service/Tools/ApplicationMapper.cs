using AutoMapper;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.ViewModels;

namespace JPOS.Service.Tools
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<MaterialModel, Material>().ReverseMap();
            CreateMap<Material, MaterialModel>().ReverseMap();
            CreateMap<Policy, PolicyModel>().ReverseMap();
            CreateMap<Request, RequestModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserProfileModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            //CreateMap<ProductMaterial, ProductMaterialModel>().ReverseMap();
            CreateMap<ProductMaterial, ProdMatModel>().ReverseMap();

            CreateMap<ProductMaterialModel, ProductMaterial>()
            .ReverseMap()
            .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material != null ? src.Material.Name : null));


            CreateMap<ProductMaterial, MaterialShow>()
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.HasValue ? (double?)src.Quantity.Value / 10 : null));
        }
    }
}
