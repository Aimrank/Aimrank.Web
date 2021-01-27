using System.Data;

namespace Aimrank.Application
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}