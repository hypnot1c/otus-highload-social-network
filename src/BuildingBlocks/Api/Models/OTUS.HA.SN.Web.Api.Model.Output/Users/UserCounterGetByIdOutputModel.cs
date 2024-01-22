using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// Пользователь
  /// </summary>
  public class UserCounterGetByIdOutputModel
  {
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    /// <example>e4d2e6b0-cde2-42c5-aac3-0b8316f21e58</example>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    /// <example>Имя</example>
    [JsonPropertyName("unread_messages")]
    public int UnreadMessages { get; set; }
  }
}
