using System;
using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.Api.Model.Output
{
  /// <summary>
  /// Пользователь
  /// </summary>
  public class UserGetByIdOutputModel
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
    [JsonPropertyName("first_name")]
    public string Firstname { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    /// <example>Фамилия</example>
    [JsonPropertyName("second_name")]
    public string Secondname { get; set; }
    /// <summary>
    /// Возраст
    /// </summary>
    /// <example>18</example>
    [JsonPropertyName("age")]
    public int? Age
    {
      get
      {
        var date = DateTime.SpecifyKind(DateTime.Parse(this.BirthDate), DateTimeKind.Utc);
        return (int)(DateTime.UtcNow.Date.Subtract(date).TotalDays / 365);
      }
    }
    /// <summary>
    /// Дата рождения
    /// </summary>
    /// <example>2017-02-01</example>
    [JsonPropertyName("birthdate")]
    public string BirthDate { get; set; }
    /// <summary>
    /// Интересы
    /// </summary>
    /// <example>Хобби, интересы и т.п.</example>
    [JsonPropertyName("biography")]
    public string Biography { get; set; }
    /// <summary>
    /// Город
    /// </summary>
    /// <example>Москва</example>
    [JsonPropertyName("city")]
    public string City { get; set; }
  }
}
