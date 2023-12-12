using AutoMapper;
using OTUS.HA.SN.Auth.Jwt;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.App.Auth.Model.Input;

namespace OTUS.HA.SN.Web.App.Auth.V1.MappingProfiles
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
      CreateMap<LoginInputModel, LoginQuery>()
        ;

      CreateMap<LoginQueryResult, UserPrincipalModel>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
      ;
    }
  }
}
