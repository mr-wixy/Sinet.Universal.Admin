using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Sinet.Universal.Admin
{
    public interface IServerInfoAppService : IApplicationService, ITransientDependency
    {
        Task<bool> GetServerStatus(CancellationToken cancellationToken = default);
    }
}
