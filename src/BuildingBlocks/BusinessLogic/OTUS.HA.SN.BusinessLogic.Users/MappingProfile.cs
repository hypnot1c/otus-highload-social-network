using System;
using AutoMapper;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<UserRegistationCommand, UserModel>()
        .ForMember(d => d.PublicId, opt => opt.MapFrom(s => Guid.NewGuid()))
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.PasswordHash, opt => opt.Ignore())
        ;

      CreateMap<UserModel, UserRegistationCommandResult>()
        .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<UserModel, UserGetByIdQueryResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<UserModel, LoginQueryResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        ;
    }
  }
}
