using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.DataService
{
  public interface IDataService
  {
    Task<IEnumerable<PostModel>> Post_FeedGetForUser(Guid userId, int offset, int limit, CancellationToken cancellationToken);
  }
}
