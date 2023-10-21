using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class MasterContext : DbContext
  {
    public MasterContext(DbContextOptions<MasterContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterContext).GetTypeInfo().Assembly);
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<FriendsModel> Friends { get; set; }
    public DbSet<PostModel> Posts { get; set; }
  }
}
