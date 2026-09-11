using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SupportTicket.Core.Models;
using System.Data;

namespace SupportTicket.Infrastructure.AdoRepository
{
    public class AdoRepo : IAdoRepo
    {
        private readonly string _connectionString;
        public AdoRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultString")!;
        }

        public IEnumerable<CustomerTicket> GetCustomerTicketDetails(int id) {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<CustomerTicket> CustomerTickets = new List<CustomerTicket>();
                using SqlCommand cmd = new SqlCommand("GivenCustomerTicketDetails",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = id;
                cmd.Connection = con;
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CustomerTickets.Add(new CustomerTicket
                    {
                        TicketId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString()!,
                        Priority = Convert.ToInt32(reader["Priority"]),
                        Status = Convert.ToInt32(reader["Status"])
                    });

                }
                return CustomerTickets;
            }
        }
        public bool UpdateTicketStatus(int id, int newStatus)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdatingTicketStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TicketId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@NewStatus", SqlDbType.Int).Value = newStatus;
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}
