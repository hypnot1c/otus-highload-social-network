using System;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserGetByIdQueryResult : BaseRequestResult
  {
    public UserGetByIdQueryResult()
    {

    }
    public UserGetByIdQueryResult(
      ResultError error
      )
    {
      this.Error = error;
    }

    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Secondname { get; set; }
    private DateTime _birthDate;
    public DateTime BirthDate
    {
      get
      {
        return _birthDate;
      }
      set
      {
        _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
      }
    }
    public string Biography { get; set; }
    public string City { get; set; }
  }
}
