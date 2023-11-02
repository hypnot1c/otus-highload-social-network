namespace OTUS.HA.SN.Web.AsyncApi.Versions.V1
{
  public class PostsHub : BaseHub<IPostsHubClient>
  {
    public PostsHub(
      ILogger<PostsHub> logger)
      : base(logger)
    {
    }

    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();

      await this.Groups.AddToGroupAsync(this.Context.ConnectionId, $"{this.UserId}");
    }
  }
}
