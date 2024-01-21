using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Sinet.Universal.Admin
{
    public interface IUserAccountAppService : IApplicationService, ITransientDependency
    {
        Task<string> LoginByPass(string name, string pass, CancellationToken cancellationToken = default);
    }
}
