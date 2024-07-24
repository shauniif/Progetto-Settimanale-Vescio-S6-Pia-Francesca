using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string MobilePhone { get; set; }

        public string FiscalCode { get; set; }
    }
}
