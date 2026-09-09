using SupportTicket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Core.JsonServices
{
    public interface IJsonServices
    {
        Task<List<PersonSummary>> GetPeopleAsync();
        Task WriteSummaryAsync(List<PersonSummary> summary);
    }
}
