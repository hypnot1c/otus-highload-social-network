using System;

namespace OTUS.HA.SN.Data.Dialog.Model
{
  public class UserDialogModel : BaseModel
  {
    public UserDialogModel()
    {
      this.CreatedAt = DateTime.UtcNow;
    }

    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
