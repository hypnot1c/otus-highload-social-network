using System;

namespace OTUS.HS.SN.Data.Master.Model
{
  public class UserDialogModel : BaseModel
  {
    public UserDialogModel()
    {
      this.CreatedAt = DateTime.UtcNow;
    }

    public int FromUserId { get; set; }
    public UserModel FromUser { get; set; }
    public int ToUserId { get; set; }
    public UserModel ToUser { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
