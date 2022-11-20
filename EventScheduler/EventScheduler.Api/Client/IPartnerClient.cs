using System.Threading;
using System.Threading.Tasks;
using EventScheduler.Api.Models;

namespace EventScheduler.Api.Client
{
  public interface IPartnerClient
  {
    public Task<PartnerAvailabilities> GetPartnerAvailabilities(CancellationToken ctx);
  }
}
