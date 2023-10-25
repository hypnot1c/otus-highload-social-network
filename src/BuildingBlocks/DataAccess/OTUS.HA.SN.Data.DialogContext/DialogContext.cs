using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OTUS.HA.SN.Data.Dialog.Model;

namespace OTUS.HA.SN.Data.Dialog.Context
{
  public class DialogContext : DbContext
  {
    public DialogContext(DbContextOptions<DialogContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DialogContext).GetTypeInfo().Assembly);
    }

    public DbSet<UserDialogModel> UserDialogs { get; set; }
  }
}
