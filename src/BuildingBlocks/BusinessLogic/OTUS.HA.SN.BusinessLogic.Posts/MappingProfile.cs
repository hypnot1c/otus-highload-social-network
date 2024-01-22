using System;
using AutoMapper;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HA.SN.BusinessLogic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<PostCreateCommand, PostModel>()
        .ForMember(d => d.PublicId, opt => opt.MapFrom(s => Guid.NewGuid()))
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.CreatedAt, opt => opt.Ignore())
        .ForMember(d => d.ModifiedAt, opt => opt.Ignore())
        .ForMember(d => d.AuthorId, opt => opt.Ignore())
        .ForMember(d => d.Author, opt => opt.Ignore())
        ;

      CreateMap<PostModel, PostCreateCommandResult>()
        .IncludeBase<PostModel, BaseRequestResult>()
        .ForMember(d => d.PostId, opt => opt.MapFrom(s => s.PublicId))
        ;

      CreateMap<PostModel, PostCreatedNotification>()
        .ForMember(d => d.PostId, opt => opt.MapFrom(s => s.Id))
        .ForMember(d => d.AuthorPublicId, opt => opt.Ignore())
        ;

      CreateMap<PostModel, BaseRequestResult>()
        .ForMember(d => d.Status, opt => opt.Ignore())
        .ForMember(d => d.Error, opt => opt.Ignore())
        ;

      CreateMap<PostModel, PostGetByIdQueryResult>()
        .IncludeBase<PostModel, BaseRequestResult>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PublicId))
        .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.Author.PublicId))
        ;

      CreateMap<PostUpdateCommand, PostModel>()
        .ForMember(d => d.ModifiedAt, opt => opt.MapFrom(s => DateTime.UtcNow))
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.PublicId, opt => opt.Ignore())
        .ForMember(d => d.AuthorId, opt => opt.Ignore())
        .ForMember(d => d.Author, opt => opt.Ignore())
        .ForMember(d => d.CreatedAt, opt => opt.Ignore())
        ;
    }
  }
}
