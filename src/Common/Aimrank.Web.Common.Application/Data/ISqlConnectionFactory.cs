using System.Data;

namespace Aimrank.Web.Common.Application.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}