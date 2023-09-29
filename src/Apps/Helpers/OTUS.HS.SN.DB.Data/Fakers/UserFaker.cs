using Bogus;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.DB.Data.Fakers
{
  public class UserFaker : Faker<UserModel>
  {
    public UserFaker(string password)
    {
      RuleFor(u => u.PublicId, f => f.Random.Guid());
      RuleFor(u => u.Firstname, f => f.Person.FirstName);
      RuleFor(u => u.Secondname, f => f.Person.LastName);
      RuleFor(u => u.Biography, f => f.Person.Website);
      RuleFor(u => u.BirthDate, f => f.Person.DateOfBirth);
      RuleFor(u => u.City, f => f.Address.City());
      RuleFor(u => u.PasswordHash, f => password);
    }
  }
}
