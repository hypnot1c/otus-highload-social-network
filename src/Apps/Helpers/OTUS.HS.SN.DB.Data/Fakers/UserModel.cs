namespace OTUS.HS.SN.DB.Data.Fakers
{
  public class UserModel
  {
    public UserModel()
    {
    }

    public int Id { get; set; }
    public Guid PublicId { get; set; }
    public string Firstname { get; set; }
    public string Secondname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public string PasswordHash { get; set; }
  }
}
