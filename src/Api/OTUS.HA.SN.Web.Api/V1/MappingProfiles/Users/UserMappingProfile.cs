using AutoMapper;
using OTUS.HA.SN.Auth.Jwt;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.MappingProfiles
{
  /// <summary>
  /// 
  /// </summary>
  public class UserMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public UserMappingProfile()
    {
      CreateMap<UserRegistrationInputModel, UserRegistationCommand>()
        ;

      CreateMap<UserRegistationCommandResult, UserRegistrationOutputModel>()
        .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.ToString()))
        ;

      CreateMap<UserGetByIdQueryResult, UserGetByIdOutputModel>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
        .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.ToShortDateString()))
        .ForMember(d => d.Age, opt => opt.Ignore())
        ;

      CreateMap<LoginInputModel, LoginQuery>()
        ;

      CreateMap<LoginQueryResult, UserPrincipalModel>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.Firstname} {s.Secondname}"));
      ;

      CreateMap<UserSearchQueryResult, UserSearchOutputModel>()
        .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
        ;
    }
  }
}
