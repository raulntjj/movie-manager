using AutoMapper;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Profiles;

public class CinemaProfile : Profile
{
	public CinemaProfile()
	{
		CreateMap<CreateCinemaDTO, Cinema>();
		CreateMap<UpdateCinemaDTO, Cinema>();
		CreateMap<Cinema, UpdateCinemaDTO>();
		CreateMap<Cinema, ReadCinemaDTO>()
			.ForMember(cinemaDTO => cinemaDTO.Address,
				opt => opt.MapFrom(cinema => cinema.Address))
			.ForMember(cinemaDTO => cinemaDTO.Sessions,
				opt => opt.MapFrom(cinema => cinema.Sessions));
	}
}