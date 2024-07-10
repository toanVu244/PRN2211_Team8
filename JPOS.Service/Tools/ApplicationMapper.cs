using AutoMapper;
using JPOS.Model.Entities;
using JPOS.Model.Models;


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
            CreateMap<ProductMaterial,ProductMaterialModel>().ReverseMap();
            CreateMap<ProductMaterial, ProdMatModel>().ReverseMap();
        }
    }
}
