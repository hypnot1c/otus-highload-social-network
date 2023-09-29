using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
  public static class StringExtensions
  {
    public static string GetPasswordHash(this string password)
    {
      var bytes = Encoding.Unicode.GetBytes(password);
      var hash = SHA256.HashData(bytes);

      string hashString = String.Join(
        String.Empty,
        hash.Select(s => String.Format("{0:x2}", s))
        );

      return hashString;
    }
  }
}
