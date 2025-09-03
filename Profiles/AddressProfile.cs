using AutoMapper;
using movies_api.Data.DTOs;
using movies_api.Models;

namespace movies_api.Profiles;

public class AddressProfile : Profile
{
	public AddressProfile()
	{
		CreateMap<CreateAddressDTO, Address>();
		CreateMap<UpdateAddressDTO, Address>();
		CreateMap<Address, UpdateAddressDTO>();
		CreateMap<Address, ReadAddressDTO>();
	}
}