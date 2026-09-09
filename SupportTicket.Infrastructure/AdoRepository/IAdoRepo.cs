using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Infrastructure.AdoRepository
{
    public interface IAdoRepo
    {
        public IEnumerable<CustomerTicket> GetCustomerTicketDetails(int Id);
        public bool UpdateTicketStatus(int Id, int NewStatus);
    }
}
