using AutoMapper;
using eMenu.Models.Authentication;
using eMenu.Models.ViewModels;

namespace eMenu.AutoMappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<AppUserViewModel,AppUser>();
			CreateMap<AppUser,AppUserViewModel>();
		}
	}
}
