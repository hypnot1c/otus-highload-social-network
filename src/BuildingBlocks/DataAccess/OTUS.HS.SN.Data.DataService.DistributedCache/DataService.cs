using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using OTUS.HS.SN.Data.Master.Context;
using OTUS.HS.SN.Data.Master.Model;

namespace OTUS.HS.SN.Data.DataService
{
  public class DataService : IDataService
  {
    public DataService(
      IDistributedCache cache,
      Slave1Context slave1Context
      )
    {
      Cache = cache;
      Slave1Context = slave1Context;
    }

    protected IDistributedCache Cache { get; }
    protected Slave1Context Slave1Context { get; }

    public async Task<IEnumerable<PostModel>> Post_FeedGetForUser(Guid userId, int offset, int limit, CancellationToken cancellationToken)
    {
      var result = await this.Cache.GetOrCreate<IEnumerable<PostModel>>($"feed-{userId}", null, cancellationToken);

      return result?.Skip(offset).Take(limit);
    }
  }
}
