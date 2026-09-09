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
        public IEnumerable<Ticket> GetAllTickets();
        public Ticket? GetTicketById(int id);
        public void AddTicket(Ticket ticket);
        public void UpdateTicket(Ticket ticket);
        public void DeleteTicket(Ticket ticket);
        public IEnumerable<Customer> GetAllCustomers();
        public Customer? GetCustomerById(int id);
        public void AddCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public void DeleteCustomer(int id);
        public void Save();

    }
}
