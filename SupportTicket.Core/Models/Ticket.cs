using SupportTicket.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Models
{
    public class Ticket:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public DateTime? ClosedAt { get; set; }
        public Customer Customer { get; set; } = null!;
        public Agent? Agent { get; set; }
        public ICollection<TicketNote> TicketNotes { get; set; }
                        = new List<TicketNote>();

    }
}
