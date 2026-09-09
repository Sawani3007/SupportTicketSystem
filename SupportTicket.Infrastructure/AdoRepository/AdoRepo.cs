
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.AdoRepository
{
    public class AdoRepo : IAdoRepo
    {
        private readonly string _connectionString;
        public AdoRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultString")!;
        }

        public IEnumerable<CustomerTicket> GetCustomerTicketDetails(int Id) {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<CustomerTicket> CustomerTickets = new List<CustomerTicket>();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GivenCustomerTicketDetails";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Id);
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (!reader.Read())
                {
                    CustomerTicket ticketDetailsDto = new CustomerTicket
                    {
                        TicketId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString()!,
                        Priority = Convert.ToInt32(reader["Priority"]),
                        Status = Convert.ToInt32(reader["Status"])
                    };

                }
                return CustomerTickets;
            }
        }
        public bool UpdateTicketStatus(int Id, int NewStatus)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UpdatingTicketStatus";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TicketId", Id);
                cmd.Parameters.AddWithValue("@NewStatus", NewStatus);
                cmd.Connection = con;
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
    }
}
