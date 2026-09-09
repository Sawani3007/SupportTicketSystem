using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;

namespace SupportTicket.Core.Interfaces
{
    public interface ITicketSorter
    {
        public IEnumerable<Ticket> Sort(IEnumerable<Ticket> tickets, TicketSortBy sortBy);
    }
}
