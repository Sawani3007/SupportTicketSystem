using SupportTicket.Core.Enums;
using SupportTicket.Core.LinQQueries;
using SupportTicket.Core.Models;
using SupportTicket.Core.Sorter;
using SupportTicket.Core.JsonServices;

var tickets = new List<Ticket>
{
    new Ticket
    {
        Id = 1,
        Title = "Login Issue",
        Description = "Customer cannot login",
        Priority = TicketPriority.High,
        Status = TicketStatus.Open,
        CustomerId = 1,
        AgentId = 1,
        CreatedAt = DateTime.Now.AddDays(-3)
    },
    new Ticket
    {
        Id = 2,
        Title = "Payment Failed",
        Description = "Payment transaction failed",
        Priority = TicketPriority.Medium,
        Status = TicketStatus.InProgress,
        CustomerId = 2,
        AgentId = 2,
        CreatedAt = DateTime.Now.AddDays(-2)
    },
    new Ticket
    {
        Id = 3,
        Title = "Password Reset",
        Description = "Customer needs password reset",
        Priority = TicketPriority.Low,
        Status = TicketStatus.Resolved,
        CustomerId = 1,
        AgentId = 1,
        CreatedAt = DateTime.Now.AddDays(-1)
    }
};

var queries = new Queries();
var sorter = new TicketSorter();

Console.WriteLine(" LINQ DEMOS ");

Console.WriteLine("\nOpen Tickets:");
foreach (var ticket in queries.GetOpenTickets(tickets))
{
    Console.WriteLine($"{ticket.Id} - {ticket.Title}");
}

Console.WriteLine("\nSearch by Title:");
foreach (var ticket in queries.SearchByTitle(tickets, "login"))
{
    Console.WriteLine($"{ticket.Id} - {ticket.Title}");
}

Console.WriteLine("\nTicket Count by Status:");
foreach (var item in queries.CountByStatus(tickets))
{
    Console.WriteLine($"{item.Status} - {item.Count}");
}

Console.WriteLine("\nTickets Sorted by Title:");
foreach (var ticket in queries.SortByTickets(
    tickets,
    sorter,
    TicketSortBy.Title))
{
    Console.WriteLine($"{ticket.Id} - {ticket.Title}");
}

Console.WriteLine("\nTicket Count by Agent:");
foreach (var item in queries.CountByAgent(tickets))
{
    Console.WriteLine($"Agent {item.AgentId} - {item.Count} tickets");
}

Console.WriteLine("\nFind by Title:");
var foundTicket = queries.FindByTitle(tickets, "Payment Failed");

if (foundTicket != null)
{
    Console.WriteLine($"{foundTicket.Id} - {foundTicket.Title}");
}

Console.WriteLine("\nAny High Priority Open Ticket?");
Console.WriteLine(
    queries.AnyHighPriorityOpenTicket(tickets));

Console.WriteLine("\n STAR WARS API ");

using var httpClient = new HttpClient();
var jsonService = new JsonServices(httpClient);

var people = await jsonService.GetPeopleAsync();

if (people.Count > 0)
{
    Console.WriteLine("\nFirst 5 People:");

    foreach (var person in people)
    {
        Console.WriteLine($"{person.Name} - Height: {person.Height}");
    }
    await jsonService.WriteSummaryAsync(people);

    Console.WriteLine("\nSummary written to people-summary.json");
}
else
{
    Console.WriteLine(
        "\nStar Wars API request failed.");
    Console.WriteLine(
        "Error summary written to people-error.json");
}