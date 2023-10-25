using AutoMapper;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.MappingProfiles
{
  /// <summary>
  /// 
  /// </summary>
  public class DialogMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public DialogMappingProfile()
    {
      CreateMap<DialogSendInputModel, DialogSendCommand>()
        .ForMember(d => d.ToUserId, opt => opt.Ignore())
        .ForMember(d => d.FromUserId, opt => opt.Ignore())
        ;

      CreateMap<DialogMessageGetQueryResult, DialogMessageGetOutputModel>()
        .ForMember(d => d.From, opt => opt.MapFrom(s => s.From.ToString()))
        .ForMember(d => d.To, opt => opt.MapFrom(s => s.To.ToString()))
        ;

      CreateMap<DialogGetQueryResult, DialogGetOutputModel>()
        ;
    }
  }
}
