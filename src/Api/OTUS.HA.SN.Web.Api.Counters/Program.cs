using AutoMapper;
using Correlate.AspNetCore;
using Microsoft.OpenApi.Models;
using OTUS.HA.SN.Web.Api.Counters.Resources;
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

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

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

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();
