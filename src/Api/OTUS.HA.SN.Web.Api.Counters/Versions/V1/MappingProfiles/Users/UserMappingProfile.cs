using AutoMapper;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.Counters.V1.MappingProfiles
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
      CreateMap<UserCounterGetByIdQueryResult, UserCounterGetByIdOutputModel>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
        ;
    }
  }
}
