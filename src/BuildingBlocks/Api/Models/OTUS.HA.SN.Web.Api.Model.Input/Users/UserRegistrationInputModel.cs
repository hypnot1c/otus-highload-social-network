using System;
using System.Text.Json.Serialization;

namespace OTUS.HA.SN.Web.Api.Model.Input
{
  /// <summary>
  /// 
  /// </summary>
  public class UserRegistrationInputModel
  {
    /// <summary>
    /// 
    /// </summary>
    /// <example>Имя</example>
    [JsonPropertyName("first_name")]
    public string Firstname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <example>Фамилия</example>
    [JsonPropertyName("second_name")]
    public string Secondname { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <example>18</example>
    [Obsolete]
    [JsonPropertyName("age")]
    public int? Age { get; set; }
    private DateTime _birthDate;
    /// <summary>
    /// Дата рождения
    /// </summary>
    /// <example>2017-02-01</example>
    [JsonPropertyName("birthdate")]
    public DateTime BirthDate
    {
      get
      {
        return _birthDate;
      }
      set
      {
        _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <example>Хобби, интересы и т.п.</example>
    [JsonPropertyName("biography")]
    public string Biography { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <example>Москва</example>
    [JsonPropertyName("city")]
    public string City { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <example>Секретная строка</example>
    [JsonPropertyName("password")]
    public string Password { get; set; }
  }
}
