using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Auth.Model;

namespace OTUS.HS.SN.Data.Auth.Context
{
  public class AuthContext : DbContext
  {
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthContext).GetTypeInfo().Assembly);
    }

    public DbSet<UserModel> Users { get; set; }
  }
}
