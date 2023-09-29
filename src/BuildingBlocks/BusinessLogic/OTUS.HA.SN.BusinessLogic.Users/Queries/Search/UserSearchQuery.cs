using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class UserSearchQuery : IRequest<UserSearchQueryResult>
  {
    public UserSearchQuery(string firstname, string lastname)
    {
      Firstname = firstname?.ToLower();
      Lastname = lastname?.ToLower();
    }

    public string Firstname { get; }
    public string Lastname { get; }
  }
}
