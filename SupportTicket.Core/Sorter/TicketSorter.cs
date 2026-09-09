using SupportTicket.Core.Enums;
using SupportTicket.Core.Interfaces;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Sorter
{
    public class TicketSorter:ITicketSorter
    {
        public IEnumerable<Ticket> Sort(IEnumerable<Ticket> tickets, TicketSortBy sortBy)
        {
            return sortBy switch
            {
                TicketSortBy.Title => tickets.OrderBy(t => t.Title),
                TicketSortBy.Priority => tickets.OrderBy(t => t.Priority),
                TicketSortBy.CreatedAt => tickets.OrderBy(t => t.CreatedAt),
                _ => tickets
            };
        }
    }
}
