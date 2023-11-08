using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Sinet.Universal.Admin
{
    [AllowAnonymous]
    public class ServerInfoAppService : ApplicationService, IServerInfoAppService
    {
        private readonly ILogger<ServerInfoAppService> _logger;

		public ServerInfoAppService(
            ILogger<ServerInfoAppService> logger
			)
		{
			_logger = logger;
		}

        public async Task<bool> GetServerStatus(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("GetServerStatus");
            return await Task.FromResult(false);
        }
    }
}
