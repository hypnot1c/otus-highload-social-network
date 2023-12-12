using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HS.SN.Data.Auth.Model;

namespace OTUS.HS.SN.Data.Auth.Context
{
  public class UserConfiguration : IEntityTypeConfiguration<UserModel>
  {
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
      builder.ToTable("User");

      builder.HasKey(p => p.Id);
    }
  }
}
