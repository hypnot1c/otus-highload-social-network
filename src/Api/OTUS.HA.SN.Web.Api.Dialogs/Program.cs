using AutoMapper;
using Microsoft.OpenApi.Models;
using OTUS.HA.SN.Web.Api.Resources;

var builder = WebApplication.CreateBuilder(args);

typeof(Program)
  .Assembly
  .GetTypes()
  .Where(t =>
    t.IsClass
    &&
    !t.IsAbstract
    &&
    t.GetInterfaces().Any(ti => ti == typeof(IWebApplicationBuilderConfigurator))
    )
  .Select(Activator.CreateInstance)
  .ToList()
  .ForEach(t => t?.GetType().GetMethod("AddServices")?.Invoke(t, new object[] { builder, builder.Configuration }))
;

builder.Services.AddTransient<DialogDataBaseMigrator>();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

using (var scope = app.Services.CreateScope())
{
  var dialogMigrator = scope.ServiceProvider.GetRequiredService<DialogDataBaseMigrator>();
  await dialogMigrator.MigrateDatabase();
}

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.UseSwagger(c =>
{
  c.RouteTemplate = "docs/{documentName}/swagger.json";
  c.PreSerializeFilters.Add((swagger, httpReq) =>
  {
    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
  });
});

app.UseAuthentication();
app.UseAuthorization();


await app.RunAsync();
