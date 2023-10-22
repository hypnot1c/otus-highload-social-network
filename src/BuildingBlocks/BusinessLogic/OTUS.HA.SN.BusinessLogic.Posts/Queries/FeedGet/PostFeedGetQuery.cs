using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostFeedGetQuery : IRequest<PostFeedGetQueryResult>
  {
    public Guid UserId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
  }
}
