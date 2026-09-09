using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.Interfaces
{
    public interface IJsonServices
    {
        Task<List<PersonSummary>> GetPeopleAsync();
        Task WriteSummaryAsync(List<PersonSummary> summary);
    }
}
