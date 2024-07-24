using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DBContext
{
    public static class Helpers
    {
        public static IServiceCollection RegisterDAO(this IServiceCollection services) 
        {
            return services
                .AddScoped<IUserDao, UserDao>()
                .AddScoped<IRoleDao, RoleDao>()
                .AddScoped<IUserRoleDao, UserRoleDao>()
                .AddScoped<ICustomerDao, CustomerDao>()
                .AddScoped<IRoomDao, RoomDao>()
                .AddScoped<IBookingDao, BookingDao>()
                .AddScoped<IAdditionalServiceDao, AdditionalServicesDao>();
        }
    }
}
