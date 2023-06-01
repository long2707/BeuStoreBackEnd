using AutoMapper;
using BeuStoreApi.Entities;
using BeuStoreApi.Models.CategoriesModel;

namespace BeuStoreApi.Mapper
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categories, CategoriesDTO>();
        }
    }
}
