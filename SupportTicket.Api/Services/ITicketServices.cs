using SupportTicket.Api.DTOs;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Api.Services
{
    public interface ITicketServices
    {
        PagedResult<TicketResponseDto> GetAllTickets(int page, int pageSize, string? search, TicketStatus? status, TicketPriority? priority);
          TicketResponseDto? GetTicketById(int id);
          TicketResponseDto AddTicket(TicketCreateDto ticketCreateDto);
          TicketResponseDto? UpdateTicket(TicketUpdateDto ticketUpdateDto, int id);
          bool DeleteTicket(int id);
          IEnumerable<CustomerResponseDto> GetAllCustomers();
          CustomerResponseDto? GetCustomerById(int id);
          CustomerResponseDto AddCustomer(CustomerCreateDto customerCreateDto);
          CustomerResponseDto? UpdateCustomer(CustomerUpdateDto customerUpdateDto, int id);
          string DeleteCustomer(int id);
          void Save();
          IEnumerable<CustomerTicketResponseDto> GetCustomerTicketDetails(int id);
          bool UpdateTicketStatus(int id, int newStatus);
    }
}
