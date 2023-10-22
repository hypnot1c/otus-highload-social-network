namespace OTUS.HA.SN.Web.Api.Resources.HostedServices
{
  /// <summary>
  /// 
  /// </summary>
  public class QueuedHostedService<T> : BackgroundService where T : class
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="taskQueue"></param>
    /// <param name="logger"></param>
    public QueuedHostedService(
      IServiceScopeFactory serviceScopeFactory,
      IBackgroundTaskQueue<T> taskQueue,
      ILogger<QueuedHostedService<T>> logger
      )
    {
      this._serviceScopeFactory = serviceScopeFactory;
      TaskQueue = taskQueue;
      _logger = logger;
    }

    private readonly ILogger<QueuedHostedService<T>> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// 
    /// </summary>
    public IBackgroundTaskQueue<T> TaskQueue { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation(
          $"Queued Hosted Service is running.{Environment.NewLine}" +
          $"background queue.{Environment.NewLine}");

      await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        var workItem = await TaskQueue.Dequeue(stoppingToken);

        try
        {
          using var scope = _serviceScopeFactory.CreateScope();
          var sp = scope.ServiceProvider;

          var targetType = typeof(IBackgroundTaskHandler<>).MakeGenericType(workItem.GetType());

          var handler = sp.GetService(targetType);

          if (handler is null)
          {
            this._logger.LogWarning($"Not found handler for task {workItem.GetType().Name}");
            continue;
          }

          await Task.Yield();
          await (ValueTask)targetType.InvokeMember("Handle", System.Reflection.BindingFlags.InvokeMethod, null, handler, new object[] { workItem, stoppingToken });
        }
        catch (Exception ex)
        {
          _logger.LogError(ex,
              "Error occurred executing {WorkItem}.", nameof(workItem));
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Queued Hosted Service is stopping.");

      await base.StopAsync(stoppingToken);
    }
  }
}
