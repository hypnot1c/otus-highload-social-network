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
        .ForMember(d => d.FriendOnes, opt => opt.Ignore())
        .ForMember(d => d.FriendTwos, opt => opt.Ignore())
        .ForMember(d => d.Posts, opt => opt.Ignore())
        .ForMember(d => d.FromDialogs, opt => opt.Ignore())
        .ForMember(d => d.ToDialogs, opt => opt.Ignore())
        ;

      CreateMap<UserModel, UserRegistationCommandResult>()
        .IncludeBase<UserModel, BaseRequestResult>()
        .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<UserModel, BaseRequestResult>()
        .ForMember(d => d.Status, opt => opt.Ignore())
        .ForMember(d => d.Error, opt => opt.Ignore())
        ;

      CreateMap<UserModel, UserGetByIdQueryResult>()
        .IncludeBase<UserModel, BaseRequestResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<UserModel, LoginQueryResult>()
        .IncludeBase<UserModel, BaseRequestResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        ;
    }
  }
}
