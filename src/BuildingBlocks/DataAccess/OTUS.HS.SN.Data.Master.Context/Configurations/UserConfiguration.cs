using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class UserConfiguration : IEntityTypeConfiguration<UserModel>
  {
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
      builder.ToTable("User");
    }
  }
}
