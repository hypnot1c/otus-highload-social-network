using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class UserDialogConfiguration : IEntityTypeConfiguration<UserDialogModel>
  {
    public void Configure(EntityTypeBuilder<UserDialogModel> builder)
    {
      builder.ToTable("User_Dialog");

      builder.HasKey(p => p.Id);

      builder.Property(p => p.FromUserId).HasColumnName("From_UserId");
      builder.Property(p => p.ToUserId).HasColumnName("To_UserId");

      builder.HasOne(p => p.FromUser).WithMany(u => u.FromDialogs).HasForeignKey(p => p.FromUserId);
      builder.HasOne(p => p.ToUser).WithMany(u => u.ToDialogs).HasForeignKey(p => p.ToUserId);
    }
  }
}
