using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OTUS.HA.SN.Auth.Jwt;
using OTUS.HA.SN.Web.Api.Resources;
using OTUS.HS.SN.Data.Master.Context;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.Configure<JwtConfigurationOptions>(builder.Configuration.GetSection("Jwt"));


builder.Services.AddControllers(opt =>
{
  var cacheProfile = new CacheProfile()
  {
    NoStore = true
  };
  opt.CacheProfiles.Add("Default", cacheProfile);
  opt.Filters.Add(new AuthorizeFilter());
  opt.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(opts =>
{
  opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
})
;

builder.Services.AddFluentValidationAutoValidation(fv =>
{
  fv.DisableDataAnnotationsValidation = true;
  ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
})
.AddValidatorsFromAssemblyContaining<OTUS.HA.SN.Web.Api.Model.Input.Validation.AssemblyMarker>()
;

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(o =>
{
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true
  };
});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<MasterContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("MasterContext"))
  );
//builder.Services.AddDbContext<MasterContext>(options => options.UseInMemoryDatabase("Master"));

builder.Services.AddAutoMapper(cfg =>
{
  cfg.AddMaps(
    typeof(Program),
    typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker)
    );
});


builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(typeof(OTUS.HA.SN.BusinessLogic.Users.AssemblyMarker).Assembly);
});


builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social network Api", Version = "v1" });

  var files = new string[]
  {
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Model.Input.AssemblyMarker).Assembly.GetName().Name}.xml",
    $"{typeof(OTUS.HA.SN.Web.Api.Model.Output.AssemblyMarker).Assembly.GetName().Name}.xml"
  };

  foreach (var file in files)
  {
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file));
  }

});

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

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


app.Run();
