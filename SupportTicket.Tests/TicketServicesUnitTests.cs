using SupportTicket.Api.DTOs;
using SupportTicket.Api.Services;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using SupportTicket.Infrastructure.AdoRepository;
using SupportTicket.Infrastructure.EFRepository;

namespace SupportTicket.Tests;

public class TicketServicesUnitTests
{
    [Theory]
    [InlineData(99)]
    [InlineData(100)]
    [InlineData(500)]
    public void GetTicketById_WhenTicketDoesNotExist_ReturnsNull(int id)
    {
        var repo = new FakeEfRepo();
        var service = new TicketServices(repo, new FakeAdoRepo());
        var result = service.GetTicketById(id);
        Assert.Null(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(5)]
    public void GetTicketById_ReturnsCorrectAgentId(int? agentId)
    {
        var repo = new FakeEfRepo();
        repo.Tickets.Add(new Ticket
        {
            Id = 1,
            Title = "Login issue",
            Description = "Unable to login",
            Priority = TicketPriority.High,
            Status = TicketStatus.Open,
            CustomerId = 1,
            AgentId = agentId
        });

        var service = new TicketServices(repo, new FakeAdoRepo());
        var result = service.GetTicketById(1);
        Assert.NotNull(result);
        Assert.Equal(agentId, result.AgentId);
        Assert.Equal("Login issue", result.Title);
    }

    [Fact]
    public void AddTicket_WhenPriorityIsInvalid_ThrowsArgumentException()
    {
        var repo = new FakeEfRepo();
        var service = new TicketServices(repo, new FakeAdoRepo());
        var ticket = new TicketCreateDto
        {
            Title = "Printer issue",
            Description = "Printer is not working",
            Priority = (TicketPriority)999,
            Status = TicketStatus.Open,
            CustomerId = 1
        };
        Assert.Throws<ArgumentException>(() => service.AddTicket(ticket));
    }

    [Fact]
    public void UpdateCustomer_WhenEmailIsChanged_UpdatesCustomerDetails()
    {
        var repo = new FakeEfRepo();
        repo.Customers.Add(new Customer
        {
            Id = 1,
            Name = "Ishika",
            Email = "ishika@example.com",
            Phone = "98765432109"
        });
        var service = new TicketServices(repo, new FakeAdoRepo());
        var customer = new CustomerUpdateDto
        {
            Name = "Ishika",
            Email = "ishika.new@example.com",
            Phone = "98765432109"
        };
        var result = service.UpdateCustomer(customer, 1);
        Assert.NotNull(result);
        Assert.Equal("ishika.new@example.com", result.Email);
        Assert.Equal("Ishika", result.Name);
        Assert.Equal("98765432109", result.Phone);
    }
    [Fact]
    public void DeleteCustomer_WhenNoOpenTickets_DeletesCustomer()
    {
        var repo = new FakeEfRepo();
        repo.Customers.Add(new Customer
        {
            Id = 1,
            Name = "Bhawana",
            Email = "bhawana@example.com",
            Phone = "98123456789"
        });
        repo.HasOpenTicketsResult = false;
        var service = new TicketServices(repo, new FakeAdoRepo());
        var result = service.DeleteCustomer(1);
        Assert.True(result);
        Assert.Empty(repo.Customers);
        Assert.Contains(1, repo.DeletedCustomerIds);
    }
    private class FakeEfRepo : IEFRepo
    {
        public List<Customer> Customers { get; } = new();
        public List<Ticket> Tickets { get; } = new();
        public List<int> DeletedCustomerIds { get; } = new();
        public bool HasOpenTicketsResult { get; set; }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return Customers;
        }
        public Customer? GetCustomerById(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }
        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
        }
        public void DeleteCustomer(int id)
        {
            DeletedCustomerIds.Add(id);

            var customer = Customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
                Customers.Remove(customer);
        }
        public bool HasOpenTickets(int id)
        {
            return HasOpenTicketsResult;
        }

        public Ticket? GetTicketById(int id)
        {
            return Tickets.FirstOrDefault(t => t.Id == id);
        }

        public void Save()
        {
        }

        public PagedResult<Ticket> GetAllTickets(
            int page,
            int pageSize,
            string? search,
            TicketStatus? status,
            TicketPriority? priority)
        {
            throw new NotImplementedException();
        }
        public void AddTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
        public void UpdateTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
        public void DeleteTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }

    private class FakeAdoRepo : IAdoRepo
    {
        public IEnumerable<CustomerTicket> GetCustomerTicketDetails(int Id)
        {
            return Enumerable.Empty<CustomerTicket>();
        }
       public bool UpdateTicketStatus(int Id, int NewStatus)
        {
            return false;
        }
    }
}