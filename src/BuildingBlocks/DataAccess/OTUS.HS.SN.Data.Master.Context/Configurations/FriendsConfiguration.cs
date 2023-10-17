using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class FriendsConfiguration : IEntityTypeConfiguration<FriendsModel>
  {
    public void Configure(EntityTypeBuilder<FriendsModel> builder)
    {
      builder.ToTable("Friends");

      builder.HasKey(p => new { p.FriendOneId, p.FriendTwoId });
      builder.Property(p => p.FriendOneId).HasColumnName("FriendOne_UserId");
      builder.Property(p => p.FriendTwoId).HasColumnName("FriendTwo_UserId");

      builder.HasOne(p => p.FriendOne).WithMany(u => u.FriendOnes).HasForeignKey(p => p.FriendOneId);
      builder.HasOne(p => p.FriendTwo).WithMany(u => u.FriendTwos).HasForeignKey(p => p.FriendTwoId);
    }
  }
}
