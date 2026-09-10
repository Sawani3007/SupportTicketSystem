using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.EFRepository
{
    public interface IEFRepo
    {
          PagedResult<Ticket> GetAllTickets(int page, int pageSize , string? search
          ,TicketStatus? status , TicketPriority? priority);
          Ticket? GetTicketById(int id);
          void AddTicket(Ticket ticket);
          void UpdateTicket(Ticket ticket);
          void DeleteTicket(Ticket ticket);
        IEnumerable<Customer> GetAllCustomers();
          Customer? GetCustomerById(int id);
          void AddCustomer(Customer customer);
          void UpdateCustomer(Customer customer);
          void DeleteCustomer(int id);
          bool HasOpenTickets(int id);
          void Save();

    }
}
