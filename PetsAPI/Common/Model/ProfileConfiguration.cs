using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            CreateMap<DogDetails, PetDetails>();
            CreateMap<CatDetails, PetDetails>()
                .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().id))
                .ForMember(dest => dest.name, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().name))
                .ForMember(dest => dest.temperament, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().temperament))
                .ForMember(dest => dest.origin, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().origin))
                .ForMember(dest => dest.country_code, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().country_code))
                .ForMember(dest => dest.description, opts => opts.MapFrom(src => src.breeds.SingleOrDefault().description))
                .ForMember(dest => dest.image, opts => opts.MapFrom(src => new Image
                {
                    url = src.url,
                    id = src.id,
                    height = src.height,
                    width = src.width
                }));
            CreateMap<CatDetails, Image>();
            CreateMap<DogDetails, Image>();
            CreateMap<ImageDetails, Image>();

        }

    }
}
