using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
  public interface IBackgroundTaskHandler<T> where T : class
  {
    ValueTask Handle(T task, CancellationToken cancellationToken);
  }
}
