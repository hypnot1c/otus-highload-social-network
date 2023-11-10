using AutoMapper;
using OTUS.HA.SN.Data.Dialog.TarantoolModel;

namespace OTUS.HA.SN.BusinessLogic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<UserDialogModel, DialogMessageGetQueryResult>()
        .ForMember(d => d.From, opt => opt.Ignore())
        .ForMember(d => d.To, opt => opt.Ignore())
        ;
    }
  }
}
