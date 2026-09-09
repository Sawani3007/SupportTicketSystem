using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.DTOs
{
    public class CustomerTicketDto
    {
        public int TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Priority { get; set; }
        public int Status { get; set; }
    }
}
