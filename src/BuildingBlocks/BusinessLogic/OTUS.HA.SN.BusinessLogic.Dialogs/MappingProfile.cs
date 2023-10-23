using AutoMapper;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<DialogSendCommand, UserDialogModel>()
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.CreatedAt, opt => opt.Ignore())
        .ForMember(d => d.FromUserId, opt => opt.Ignore())
        .ForMember(d => d.FromUser, opt => opt.Ignore())
        .ForMember(d => d.ToUserId, opt => opt.Ignore())
        .ForMember(d => d.ToUser, opt => opt.Ignore())
        ;

      CreateMap<UserDialogModel, DialogMessageGetQueryResult>()
        .ForMember(d => d.From, opt => opt.MapFrom(s => s.FromUser.PublicId))
        .ForMember(d => d.To, opt => opt.MapFrom(s => s.ToUser.PublicId))
        ;
    }
  }
}
