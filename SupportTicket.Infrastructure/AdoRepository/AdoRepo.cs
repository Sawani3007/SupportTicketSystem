
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SupportTicket.Core.DTOs;
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

        public IEnumerable<TicketDetailsDto> GetAllTickets()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<TicketDetailsDto> ticketDetailsDtos = new List<TicketDetailsDto>();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * From TicketDetails";
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (!reader.Read())
                {
                    TicketDetailsDto ticketDetailsDto = new TicketDetailsDto
                    {
                        TicketId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString()!,
                        Priority = Convert.ToInt32(reader["Priority"]),
                        Status = Convert.ToInt32(reader["Status"]),
                        CustomerName = reader["Name"].ToString()!,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };

                }
                return ticketDetailsDtos;
                ;
            }

        }
        public IEnumerable<CustomerTicketDto> GetCustomerTicketDetails(int Id) {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<CustomerTicketDto> customerTicketDtos = new List<CustomerTicketDto>();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GivenCustomerTicketDetails";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Id);
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (!reader.Read())
                {
                    CustomerTicketDto ticketDetailsDto = new CustomerTicketDto
                    {
                        TicketId = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString()!,
                        Priority = Convert.ToInt32(reader["Priority"]),
                        Status = Convert.ToInt32(reader["Status"])
                    };

                }
                return customerTicketDtos;
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
