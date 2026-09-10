using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SupportTicket.Core.Models
{
    public class Agent:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
