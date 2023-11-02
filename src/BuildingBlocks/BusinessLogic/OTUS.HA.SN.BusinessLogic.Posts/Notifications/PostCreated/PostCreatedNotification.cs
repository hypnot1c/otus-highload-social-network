using MediatR;

namespace OTUS.HA.SN.BusinessLogic
{
  public class PostCreatedNotification : INotification
  {
    public int PostId { get; set; }
    public int AuthorId { get; set; }
    public string Text { get; set; }
  }
}
