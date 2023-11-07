using System.Threading.Tasks;

namespace Sinet.Universal.Admin.Data;

public interface IAdminDbSchemaMigrator
{
    Task MigrateAsync();
}
