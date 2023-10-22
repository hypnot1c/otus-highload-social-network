using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
  public class BackgroundTaskQueue<T> : IBackgroundTaskQueue<T> where T : class
  {
    public BackgroundTaskQueue() : this(1000)
    {

    }
    public BackgroundTaskQueue(int capacity)
    {
      // Capacity should be set based on the expected application load and
      // number of concurrent threads accessing the queue.            
      // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
      // which completes only when space became available. This leads to backpressure,
      // in case too many publishers/calls start accumulating.
      var options = new BoundedChannelOptions(capacity)
      {
        FullMode = BoundedChannelFullMode.Wait
      };
      _queue = Channel.CreateBounded<T>(options);
    }

    private readonly Channel<T> _queue;

    public async ValueTask Enqueue(T workItem, CancellationToken cancellationToken)
    {
      if (workItem == null)
      {
        throw new ArgumentNullException(nameof(workItem));
      }

      await _queue.Writer.WriteAsync(workItem, cancellationToken);
    }

    public async ValueTask<T> Dequeue(CancellationToken cancellationToken)
    {
      var workItem = await _queue.Reader.ReadAsync(cancellationToken);

      return workItem;
    }
  }
}
