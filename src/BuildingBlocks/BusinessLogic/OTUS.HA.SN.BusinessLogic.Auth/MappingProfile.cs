using AutoMapper;
using OTUS.HS.SN.Data.Master.Model;

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
    }
  }
}
