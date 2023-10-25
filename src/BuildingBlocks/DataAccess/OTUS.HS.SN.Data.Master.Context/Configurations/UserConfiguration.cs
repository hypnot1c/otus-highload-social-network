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

      builder.HasKey(p => p.Id);

      builder.HasMany(p => p.FriendOnes).WithOne(u => u.FriendOne).HasForeignKey(p => p.FriendOneId);
      builder.HasMany(p => p.FriendTwos).WithOne(u => u.FriendTwo).HasForeignKey(p => p.FriendTwoId);
    }
  }
}
