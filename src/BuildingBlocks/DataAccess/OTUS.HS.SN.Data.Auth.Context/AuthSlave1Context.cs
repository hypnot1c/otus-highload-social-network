using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Auth.Model;

namespace OTUS.HS.SN.Data.Auth.Context
{
  public class AuthSlave1Context : DbContext
  {
    public AuthSlave1Context(DbContextOptions<AuthSlave1Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthSlave1Context).GetTypeInfo().Assembly);
    }

    public DbSet<UserModel> Users { get; set; }
  }
}
