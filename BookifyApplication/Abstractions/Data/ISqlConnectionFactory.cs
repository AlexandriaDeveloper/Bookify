using System.Data;

namespace BookifyApplication.Abstractions.Data;
internal interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
