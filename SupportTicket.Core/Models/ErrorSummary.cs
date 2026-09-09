using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Models
{
    public class ErrorSummary
    {
        public bool Success {  get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
