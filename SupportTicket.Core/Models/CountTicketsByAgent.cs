using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Models
{
    public class CountTicketsByAgent
    {
        public int AgentId { get; set; }
        public int Count {  get; set; }
    }
}
