using System;

namespace OTUS.HS.SN.Data.Auth.Model
{
  public class UserModel : BaseModel
  {
    public UserModel()
    {
    }
    public Guid PublicId { get; set; }
    public string PasswordHash { get; set; }
  }
}
