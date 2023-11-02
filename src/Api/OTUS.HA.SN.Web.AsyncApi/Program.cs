using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using OTUS.HA.SN.Kafka.Message;
using OTUS.HA.SN.Web.AsyncApi.Versions.V1;
using OTUS.HS.SN.Data.Master.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
{
  options.Authority = "http://localhost:5261";

  options.Events = new JwtBearerEvents
  {
    OnMessageReceived = context =>
    {
      var accessToken = context.Request.Query["access_token"];

      var path = context.HttpContext.Request.Path;
      if (!string.IsNullOrEmpty(accessToken) &&
          (path.StartsWithSegments("/hubs")))
      {
        context.Token = accessToken;
      }
      return Task.CompletedTask;
    }
  };
});

builder.Services.AddMassTransit(x =>
{
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.Host("rabbit");
    cfg.ConfigureEndpoints(context);
  });

  x.AddRider(rider =>
  {
    rider.AddConsumer<PostCreatedMessageConsumer>();

    rider.UsingKafka((context, k) =>
    {
      k.Host(builder.Configuration.GetConnectionString("Kafka"));

      k.TopicEndpoint<PostCreatedKafkaMessage>("posts-topic", "post-wss-consumer", e =>
      {
        e.ConfigureConsumer<PostCreatedMessageConsumer>(context);
      });
    });
  });
});

builder.Services
  .AddSignalR()
  .AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis"), options =>
  {
    options.Configuration.ChannelPrefix = "WEB_API-SignalR";
  })
  .AddJsonProtocol(opts =>
  {
    opts.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
  })
;

builder.Services.AddDbContext<MasterContext>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("MasterContext"))
    );
builder.Services.AddDbContext<Slave1Context>(options => options
  .UseNpgsql(builder.Configuration.GetConnectionString("Slave1Context"))
);

builder.Services.AddAutoMapper(cfg =>
{
  cfg.AddMaps(
    typeof(Program)
    );
});

builder.Services.AddScoped<PostsHubWrapper>();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<PostsHub>("/post/feed");

await app.RunAsync();
