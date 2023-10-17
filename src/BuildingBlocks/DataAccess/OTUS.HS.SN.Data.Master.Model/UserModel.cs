using System;
using System.Collections.Generic;

namespace OTUS.HS.SN.Data.Master.Model
{
  public class UserModel : BaseModel
  {
    public UserModel()
    {
      this.FriendOnes = new HashSet<FriendsModel>();
      this.FriendTwos = new HashSet<FriendsModel>();
    }
    public Guid PublicId { get; set; }
    public string Firstname { get; set; }
    public string Secondname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<FriendsModel> FriendOnes { get; set; }
    public ICollection<FriendsModel> FriendTwos { get; set; }
  }
}
