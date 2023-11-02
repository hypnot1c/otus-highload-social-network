using Microsoft.AspNetCore.SignalR;

namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public interface IPostsHubClient
  {
    [HubMethodName("posted")]
    Task PostCreated(PostMessage post, CancellationToken cancellationToken);
  }
}
