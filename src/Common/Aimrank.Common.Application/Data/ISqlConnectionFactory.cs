using System.Data;

namespace Aimrank.Common.Application.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}