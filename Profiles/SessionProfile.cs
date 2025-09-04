using AutoMapper;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Profiles;

public class SessionProfile : Profile
{
	public SessionProfile()
	{
		CreateMap<CreateSessionDTO, Session>();
		CreateMap<UpdateSessionDTO, Session>();
		CreateMap<Session, UpdateSessionDTO>();
		CreateMap<Session, ReadSessionDTO>();
	}
}