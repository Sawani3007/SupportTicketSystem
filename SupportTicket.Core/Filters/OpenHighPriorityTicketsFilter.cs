using SupportTicket.Core.Enums;
using SupportTicket.Core.Interfaces;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Filters
{
    public class OpenHighPriorityTicketsFilter:ITicketFilter
    {
        public IEnumerable<Ticket> Filter(IEnumerable<Ticket> tickets) { 
            return tickets.Where(t=>t.Status == TicketStatus.Open && t.Priority == TicketPriority.High);
        }
    }
}
