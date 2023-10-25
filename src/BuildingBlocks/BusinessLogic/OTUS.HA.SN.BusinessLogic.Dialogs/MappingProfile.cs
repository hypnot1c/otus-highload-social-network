using AutoMapper;
using OTUS.HA.SN.Data.Dialog.Model;

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
        .ForMember(d => d.ToUserId, opt => opt.Ignore())
        ;

      CreateMap<UserDialogModel, DialogMessageGetQueryResult>()
        .ForMember(d => d.From, opt => opt.Ignore())
        .ForMember(d => d.To, opt => opt.Ignore())
        ;
    }
  }
}
