using AutoMapper;
using OTUS.HA.SN.Web.AsyncApi.Resources;
using OTUS.HA.SN.Web.AsyncApi.Versions.V1;

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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<PostsHub>("/post/feed");

await app.RunAsync();
