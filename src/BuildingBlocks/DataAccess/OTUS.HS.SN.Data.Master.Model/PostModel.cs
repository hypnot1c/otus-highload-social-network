using System;

namespace OTUS.HS.SN.Data.Master.Model
{
  public class PostModel : BaseModel
  {
    public PostModel()
    {
      this.PublicId = Guid.NewGuid();
      this.CreatedAt = this.ModifiedAt = DateTime.UtcNow;
    }
    public Guid PublicId { get; set; }
    public int AuthorId { get; set; }
    public UserModel Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
  }
}
