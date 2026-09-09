using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Interfaces
{
    public interface ITicketFilter
    {
        IEnumerable<Ticket> Filter(IEnumerable<Ticket> tickets);
    }
}
