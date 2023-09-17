using System.Text;

namespace System.Security.Cryptography
{
  public static class StringExtensions
  {
    public static string GetPasswordHash(this string password)
    {
      var bytes = Encoding.Unicode.GetBytes(password);
      var hash = SHA256.HashData(bytes);
      string hashString = String.Empty;
      foreach (var x in hash)
      {
        hashString += String.Format("{0:x2}", x);
      }
      return hashString;
    }
  }
}
