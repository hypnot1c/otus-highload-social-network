using System;
using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostGetByIdQuery : IRequest<PostGetByIdQueryResult>
  {
    public PostGetByIdQuery(Guid id)
    {
      this.Id = id;
    }

    public Guid Id { get; set; }
  }
}
