using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services
{
    public class SqlServerServiceBase : ServiceBase
    {
        private SqlConnection _connection;
        public SqlServerServiceBase(IConfiguration config)
        {
            _connection = new SqlConnection(config.GetConnectionString("Db"));
        }
        protected override DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

        protected override DbConnection GetConnection()
        {
            return _connection;
        }
    }
}
