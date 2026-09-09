using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SupportTicket.Core.Models;
using SupportTicket.Api.DTOs;

namespace SupportTicket.Api.Services
{
    public interface ITicketServices
    {
          PagedResult<Ticket> GetAllTickets(int page, int pageSize);
          TicketResponseDto? GetTicketById(int id);
          TicketResponseDto AddTicket(TicketCreateDto ticketCreateDto);
          TicketResponseDto? UpdateTicket(TicketUpdateDto ticketUpdateDto, int id);
          bool DeleteTicket(int id);
          IEnumerable<CustomerResponseDto> GetAllCustomers();
          CustomerResponseDto? GetCustomerById(int id);
          CustomerResponseDto AddCustomer(CustomerCreateDto customerCreateDto);
          CustomerResponseDto? UpdateCustomer(CustomerUpdateDto customerUpdateDto, int id);
          bool DeleteCustomer(int id);
          void Save();
          IEnumerable<CustomerTicket> GetCustomerTicketDetails(int id);
          bool UpdateTicketStatus(int id, int newStatus);
    }
}
