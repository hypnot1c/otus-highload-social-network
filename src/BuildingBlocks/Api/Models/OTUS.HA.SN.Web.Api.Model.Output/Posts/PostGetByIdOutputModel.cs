using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// Пост пользователя
  /// </summary>
  public class PostGetByIdOutputModel
  {
    /// <summary>
    /// Id поста
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Содержимое
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Id автора
    /// </summary>
    [JsonPropertyName("author_user_id")]
    public string AuthorId { get; set; }
  }
}
