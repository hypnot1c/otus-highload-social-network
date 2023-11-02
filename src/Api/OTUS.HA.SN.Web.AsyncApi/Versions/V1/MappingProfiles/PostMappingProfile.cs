using AutoMapper;
using OTUS.HA.SN.Kafka.Message;

namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public class PostMappingProfile : Profile
  {
    public PostMappingProfile()
    {
      CreateMap<PostCreatedKafkaMessage, PostMessage>()
        .ForMember(d => d.MessageId, opt => opt.Ignore())
        .ForMember(d => d.Payload, opt => opt.MapFrom(s => s))
        ;

      CreateMap<PostCreatedKafkaMessage, PostMessagePayload>()
        .ForMember(d => d.PostText, opt => opt.MapFrom(s => s.Text))
        .ForMember(d => d.PostId, opt => opt.Ignore())
        .ForMember(d => d.AuthorId, opt => opt.Ignore())
        ;
    }
  }
}
