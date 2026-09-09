using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Models
{
    public class TicketNote:BaseEntity
    {
        public int TicketId { get; set; }
        public string NoteText { get; set; } = string.Empty;
        public Ticket Ticket { get; set; } = null!;
    }
}
