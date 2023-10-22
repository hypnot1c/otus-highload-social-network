using AutoMapper;
using OTUS.HA.SN.BusinessLogic;
using OTUS.HA.SN.Web.Api.Model.Input;
using OTUS.HA.SN.Web.Api.Model.Output;

namespace OTUS.HA.SN.Web.Api.V1.MappingProfiles
{
  /// <summary>
  /// 
  /// </summary>
  public class PostMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public PostMappingProfile()
    {
      CreateMap<PostCreateInputModel, PostCreateCommand>()
        .ForMember(d => d.AuthorId, opt => opt.Ignore())
        ;

      CreateMap<PostCreateCommandResult, PostCreateOutputModel>()
        .ForMember(d => d.PostId, opt => opt.MapFrom(s => s.PostId.ToString()))
        ;

      CreateMap<PostGetByIdQueryResult, PostGetByIdOutputModel>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
        .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.AuthorId.ToString()))
        ;

      CreateMap<PostUpdateInputModel, PostUpdateCommand>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.Parse(s.Id)))
        .ForMember(d => d.UpdaterId, opt => opt.Ignore())
        ;

      CreateMap<PostFeedGetInputModel, PostFeedGetQuery>()
        .ForMember(d => d.UserId, opt => opt.Ignore())
        ;

      CreateMap<PostFeedGetQueryResult, PostFeedGetOutputModel>()
        ;

      CreateMap<PostCreatedNotification, PostCreatedBackgroundTask>()
        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PostId))
        ;
    }
  }
}
