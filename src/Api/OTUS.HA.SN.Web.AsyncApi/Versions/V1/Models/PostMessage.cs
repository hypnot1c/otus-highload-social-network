using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public class PostMessage
  {
    public string MessageId { get; set; } = "post";
    public PostMessagePayload Payload { get; set; }
  }

  public class PostMessagePayload
  {
    public Guid PostId { get; set; }
    public string PostText { get; set; }

    [JsonPropertyName("author_user_id")]
    public Guid AuthorId { get; set; }
  }
}
