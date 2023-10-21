using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class PostsConfiguration : IEntityTypeConfiguration<PostModel>
  {
    public void Configure(EntityTypeBuilder<PostModel> builder)
    {
      builder.ToTable("Post");

      builder.HasKey(p => p.Id);

      builder.Property(p => p.AuthorId).HasColumnName("Author_UserId");

      builder.HasOne(p => p.Author).WithMany(p => p.Posts).HasForeignKey(p => p.AuthorId);
    }
  }
}
