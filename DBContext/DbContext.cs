using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DBContext
{
    public class DbContext
    {
        public IUserDao User {  get; set; }
        public IRoleDao Role { get; set; }

        public IUserRoleDao RoleUser { get; set; }
        public ICustomerDao Customer { get; set; }

        public IRoomDao Rooms { get; set; }

        public IBookingDao Booking { get; set; }

        public IAdditionalServiceDao AdditionalService { get; set; }
 

        public DbContext(IUserDao userDao, IRoleDao roleDao, IUserRoleDao roleUserDao, ICustomerDao customer, IRoomDao rooms , IBookingDao booking, IAdditionalServiceDao additionalService)
        {
            User = userDao;
            Role = roleDao;
            RoleUser = roleUserDao;
            Customer = customer;
            Rooms = rooms;
            Booking = booking;
            AdditionalService = additionalService;
        }

    }
}
