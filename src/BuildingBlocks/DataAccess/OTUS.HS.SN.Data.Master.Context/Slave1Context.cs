using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.Master.Context
{
  public class Slave1Context : DbContext
  {
    public Slave1Context(DbContextOptions<Slave1Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(Slave1Context).GetTypeInfo().Assembly);
    }

    public DbSet<UserModel> Users { get; set; }
  }
}
