using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
  public interface IBackgroundTaskQueue<T> where T : class
  {
    ValueTask Enqueue(T workItem, CancellationToken cancellationToken);

    ValueTask<T> Dequeue(CancellationToken cancellationToken);
  }
}
