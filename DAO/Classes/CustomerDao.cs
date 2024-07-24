using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Protocol.Plugins;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Data;
using Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.DAO.Classes
{
    public class CustomerDao : SqlServerServiceBase, ICustomerDao
    {
        public CustomerDao(IConfiguration config) : base(config)
        {
        }

        private const string CREATE_CUS = "INSERT INTO " +
            "Customers(LastName, FirstName, City, " +
            "Province, Email, Telephone, " +
            "MobilePhone, FiscalCode) " +
            "OUTPUT INSERTED.IdCustomer " +
            "VALUES(@ln, @fn, @city, @prov, @email, @tel, @mp, @fc)";
        private const string DELETE_CUS = "DELETE FROM Customers WHERE IdCustomer = @id";
        private const string SELECT_BY_ID = "SELECT IdCustomer, LastName, FirstName, " +
            "City, Province, Email, Telephone, MobilePhone, FiscalCode " +
            "FROM Customers " +
            "WHERE IdCustomer = @id";
        private const string SELECT_ALL_CUSTOMER = "SELECT IdCustomer, LastName, FirstName, " +
            "City, Province, Email, Telephone, MobilePhone, FiscalCode " +
            "FROM Customers";
        private const string UPDATE_CUS = "UPDATE Customers SET " +
            "LastName = @ln, " +
            "FirstName = @fn, " +
            "City = @city, " +
            "Province = @prov, " +
            "Email = @email, " +
            "Telephone = @tel, " +
            "MobilePhone = @mp, " +
            "FiscalCode = @fc " + 
            "WHERE IdCustomer = @id";

        private CustomerEntity CreateReader(DbDataReader reader)
        {
            return new CustomerEntity
            {
                Id = reader.GetInt32(0),
                LastName = reader.GetString(1),
                FirstName = reader.GetString(2),
                City = reader.GetString(3),
                Province = reader.GetString(4),
                Email = reader.GetString(5),
                Telephone = reader.GetString(6),
                MobilePhone = reader.GetString(7),
                FiscalCode = reader.GetString(8),
            };
        }
        public void Paramaters(DbCommand cmd,CustomerEntity customer)
        {
            cmd.Parameters.Add(new SqlParameter("@ln", customer.LastName));
            cmd.Parameters.Add(new SqlParameter("@fn", customer.FirstName));
            cmd.Parameters.Add(new SqlParameter("@city", customer.City));
            cmd.Parameters.Add(new SqlParameter("@prov", customer.Province));
            cmd.Parameters.Add(new SqlParameter("@email", customer.Email));
            cmd.Parameters.Add(new SqlParameter("@tel", customer.Telephone));
            cmd.Parameters.Add(new SqlParameter("@mp", customer.MobilePhone));
            cmd.Parameters.Add(new SqlParameter("@fc", customer.FiscalCode));
        } 
        public CustomerEntity Create(CustomerEntity customer)
        {
            var cmd = GetCommand(CREATE_CUS);
            var conn = GetConnection();
            conn.Open();
            Paramaters(cmd, customer);
            customer.Id = (int)cmd.ExecuteScalar();
            conn.Close();
            return customer;
        }

        public CustomerEntity Delete(int id)
        {
            var deleteCust = ReadById(id);
            var cmd = GetCommand(DELETE_CUS);
            var conn = GetConnection();
            conn.Open();
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.ExecuteNonQuery();
            conn.Close();
            return deleteCust;
        }

        public List<CustomerEntity> GetAll()
        {
            List<CustomerEntity> customers = new List<CustomerEntity>();
            var cmd = GetCommand(SELECT_ALL_CUSTOMER);
            var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(CreateReader(reader));
            }
            conn.Close();
            return customers;
        }

        public CustomerEntity ReadById(int id)
        {
            var cmd = GetCommand(SELECT_BY_ID);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            CustomerEntity customer = new CustomerEntity();
            if (reader.Read())
                customer = CreateReader(reader);
            conn.Close();
            return customer;
        }

        public CustomerEntity Update(int id, CustomerEntity customer)
        {
            var cmd = GetCommand(UPDATE_CUS);
            var conn = GetConnection();
            Paramaters(cmd, customer);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            conn.Open();
            var reader = cmd.ExecuteReader();
            var c = new CustomerEntity();
            if (reader.Read())
                c = CreateReader(reader);
            conn.Close();
            return c;
        }
    }
}
