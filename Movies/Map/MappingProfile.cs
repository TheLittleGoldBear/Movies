using AutoMapper;
using Movies.Dto;
using Movies.Model;

namespace Movies.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieReadDto>()
            .ForMember(dest => dest.Directors,
                opt => opt.MapFrom(src => src.MovieDirectors.Select(md => md.Director)));


            CreateMap<Movie, MovieCreateDto>()
                .ForMember(dest => dest.Directors,
                opt => opt.MapFrom(src => src.MovieDirectors.Select(md => md.Director)));

            CreateMap<Director, DirecorInputDto>();
            CreateMap<Director, DirectorWithoutMoviesDto>();
           
            CreateMap<MovieInputDto, Movie>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieReadDto, Movie>();
        }
    }
}
