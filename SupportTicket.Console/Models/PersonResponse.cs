using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Models
{
    public class PersonResponse
    {
        public List<Person> Results { get; set; } = new();
    }
}
