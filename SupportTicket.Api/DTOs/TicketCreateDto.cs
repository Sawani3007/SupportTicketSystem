using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Api.DTOs
{
    public class TicketCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public TicketPriority Priority { get; set; }
        [Required]
        public TicketStatus Status { get; set; }
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
    }
}
