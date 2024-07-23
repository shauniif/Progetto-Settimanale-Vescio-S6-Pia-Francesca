using System.Data.Common;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services
{
    public abstract class ServiceBase
    {
        protected abstract DbConnection GetConnection();
        protected abstract DbCommand GetCommand(string commandText);

    }
}
