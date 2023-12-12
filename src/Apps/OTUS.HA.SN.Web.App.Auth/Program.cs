using AutoMapper;
using Correlate.AspNetCore;
using Microsoft.OpenApi.Models;
using OTUS.HA.SN.Web.App.Auth.Resources;
using Swashbuckle.AspNetCore.SwaggerUI;

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

builder.Services.AddTransient<AuthDataBaseMigrator>();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

using (var scope = app.Services.CreateScope())
{
  var migrator = scope.ServiceProvider.GetRequiredService<AuthDataBaseMigrator>();
  await migrator.MigrateDatabase();
}

app.UseCorrelate();

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

app.UseSwaggerUI(c =>
{
  c.RoutePrefix = "swagger";
  c.SwaggerEndpoint("/docs/v1/swagger.json", "Social network Api V1");
  c.DocExpansion(DocExpansion.Full);
});

await app.RunAsync();
