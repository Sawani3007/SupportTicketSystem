using SupportTicket.Core.Models;
using SupportTicket.Infrastructure.Data;
using System.Collections.Generic;
using SupportTicket.Core.Enums;
using System.Linq;

namespace SupportTicket.Infrastructure.EFRepository
{
    public class EFRepo : IEFRepo
    {
        private readonly TicketDbContext _context;

        public EFRepo(TicketDbContext context)
        {
            _context = context;
        }
        public PagedResult<Ticket> GetAllTickets(int page, int pageSize)
        {
            var totalCount = _context.Tickets.Count();

            var tickets = _context.Tickets
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Ticket>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = tickets
            };
        }

        public Ticket? GetTicketById(int id)
        {
            return _context.Tickets.Find(id);
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            
        }

        public void DeleteTicket(Ticket ticket)
        {
            ticket.Status = TicketStatus.Closed;
            _context.Tickets.Update(ticket);
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }
        public bool HasOpenTickets(int customerId)
        {
            return _context.Tickets
                .Any(t =>t.CustomerId == customerId &&
                    t.Status == TicketStatus.Open);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
