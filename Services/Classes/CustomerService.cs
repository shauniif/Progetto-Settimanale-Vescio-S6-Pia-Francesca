using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly DbContext _db;

        public CustomerService(DbContext db)
        {
            _db = db;
        }
        public CustomerModel DeleteCustomer(int id)
        {
            var c = _db.Customer.Delete(id);
            return new CustomerModel
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                City = c.City,
                Province = c.Province,
                Email = c.Email,
                Telephone = c.Telephone,
                MobilePhone = c.MobilePhone,
                FiscalCode = c.FiscalCode
            };
        }

        public IEnumerable<CustomerModel> GetAll()
        {
            var customers = _db.Customer.GetAll();
            return customers.Select(c => new CustomerModel
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                City = c.City,
                Province = c.Province,
                Email = c.Email,
                Telephone = c.Telephone,
                MobilePhone = c.MobilePhone,
                FiscalCode = c.FiscalCode,
            });
        }

        public CustomerModel GetCustomer(int id)
        {
            var c = _db.Customer.ReadById(id);
            return new CustomerModel
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                City = c.City,
                Province = c.Province,
                Email = c.Email,
                Telephone = c.Telephone,
                MobilePhone = c.MobilePhone,
                FiscalCode = c.FiscalCode
            };
        }

        public CustomerModel RegisterCustomer(CustomerModel customer)
        {
            var c = _db.Customer.Create(
                new CustomerEntity
                {
                    Id = customer.Id,
                    LastName= customer.LastName,
                    FirstName = customer.FirstName,
                    City = customer.City,
                    Province= customer.Province,
                    Email = customer.Email,
                    Telephone = customer.Telephone,
                    MobilePhone = customer.MobilePhone,
                    FiscalCode = customer.FiscalCode,
                });
            return new CustomerModel
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                City = c.City,
                Province = c.Province,
                Email = c.Email,
                Telephone = c.Telephone,
                MobilePhone = c.MobilePhone,
                FiscalCode = c.FiscalCode
            };
        }

        public CustomerModel UpdateCustomer(int id, CustomerModel customer)
        {
            var c = _db.Customer.Update(id,
                new CustomerEntity
                {
                    Id = id,
                    LastName = customer.LastName,
                    FirstName = customer.FirstName,
                    City = customer.City,
                    Province = customer.Province,
                    Email = customer.Email,
                    Telephone = customer.Telephone,
                    MobilePhone = customer.MobilePhone,
                    FiscalCode = customer.FiscalCode,
                });
            return new CustomerModel
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                City = c.City,
                Province = c.Province,
                Email = c.Email,
                Telephone = c.Telephone,
                MobilePhone = c.MobilePhone,
                FiscalCode = c.FiscalCode
            };
        }
    }
}
