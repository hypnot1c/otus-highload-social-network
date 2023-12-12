using AutoMapper;
using OTUS.HA.SN.BusinessLogic.Auth;
using OTUS.HS.SN.Data.Auth.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<UserModel, BaseRequestResult>()
        .ForMember(d => d.Status, opt => opt.Ignore())
        .ForMember(d => d.Error, opt => opt.Ignore())
        ;

      CreateMap<UserModel, LoginQueryResult>()
        .IncludeBase<UserModel, BaseRequestResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<UserCreateCommand, UserModel>()
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.PasswordHash, opt => opt.Ignore())
        ;
    }
  }
}
