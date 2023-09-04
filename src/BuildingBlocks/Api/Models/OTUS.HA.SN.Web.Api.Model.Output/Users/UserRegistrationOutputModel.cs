using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// 
  /// </summary>
  public class UserRegistrationOutputModel
  {
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
  }
}
