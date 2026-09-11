using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Interfaces;
using SupportTicket.Core.Models;

namespace SupportTicket.Core.LinQQueries
{
    public class Queries
    {
        public IEnumerable<Ticket> GetOpenTickets(IEnumerable<Ticket> tickets)
        {
            return tickets.Where(t=>t.Status == TicketStatus.Open);
        }
        public IEnumerable<Ticket> SearchByTitle(IEnumerable<Ticket> tickets , string searchTitle)
        {
            return tickets.Where(t => t.Title.Contains(searchTitle , StringComparison.OrdinalIgnoreCase));
        }
        public IEnumerable<CountTicketByStatus> CountByStatus(IEnumerable<Ticket> tickets)
        {
            return tickets
                .GroupBy(t => t.Status)
                .Select(group => new CountTicketByStatus
                {
                    Status = group.Key,
                    Count = group.Count()
                });
        }
        public IEnumerable<Ticket> SortByTickets(IEnumerable<Ticket> tickets , ITicketSorter sorter , TicketSortBy sortBy)
        {
            return sorter.Sort(tickets, sortBy);
        }
        public IEnumerable<CountTicketsByAgent> CountByAgent(IEnumerable<Ticket> tickets)
        {
            return tickets
                .Where(t => t.AgentId is not null)
                .GroupBy(t => t.AgentId!.Value)
                .Select(group => new CountTicketsByAgent
                {
                    AgentId = group.Key,
                    Count = group.Count()
                });
        }
        public Ticket? FindByTitle(IEnumerable<Ticket> tickets , string findTitle)
        {
            return tickets.FirstOrDefault(t=>t.Title.Equals(findTitle, StringComparison.OrdinalIgnoreCase));
        }
        public bool AnyHighPriorityOpenTicket(IEnumerable<Ticket> tickets)
        { 
            return tickets.Any(t=>t.Priority == TicketPriority.High && t.Status == TicketStatus.Open);
        }
    }
}
