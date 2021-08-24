using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Alumn, AlumnDTO>().ReverseMap();
        }
    }
}