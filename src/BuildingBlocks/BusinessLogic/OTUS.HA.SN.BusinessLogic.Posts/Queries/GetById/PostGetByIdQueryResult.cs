using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostGetByIdQueryResult : BaseRequestResult
  {
    public PostGetByIdQueryResult()
    {

    }
    public PostGetByIdQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Text { get; set; }
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
      get
      {
        return _createdAt;
      }
      set
      {
        _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
      }
    }
    private DateTime _modifiedAt;
    public DateTime ModifiedAt
    {
      get
      {
        return _modifiedAt;
      }
      set
      {
        _modifiedAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
      }
    }
  }
}
