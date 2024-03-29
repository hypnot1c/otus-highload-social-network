using AutoMapper;
using Correlate.AspNetCore;
using Microsoft.OpenApi.Models;
using OTUS.HA.SN.Web.Api.Dialogs.Resources;
using Prometheus;

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

app.UseCorrelate();

app.UseRouting();
app.UseHttpMetrics();
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

app.UseEndpoints(endpoints =>
{
  endpoints.MapMetrics();
});

await app.RunAsync();
