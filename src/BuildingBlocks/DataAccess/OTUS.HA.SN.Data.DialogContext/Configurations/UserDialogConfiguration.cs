using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HA.SN.Data.Dialog.Model;

namespace OTUS.HA.SN.Data.Dialog.Context
{
  public class UserDialogConfiguration : IEntityTypeConfiguration<UserDialogModel>
  {
    public void Configure(EntityTypeBuilder<UserDialogModel> builder)
    {
      builder.ToTable("User_Dialog");

      builder.HasKey(p => p.Id);

      builder.Property(p => p.FromUserId).HasColumnName("From_UserId");
      builder.Property(p => p.ToUserId).HasColumnName("To_UserId");
    }
  }
}
