using Moq;
using SupportTicket.Api.DTOs;
using SupportTicket.Api.Services;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using SupportTicket.Infrastructure.AdoRepository;
using SupportTicket.Infrastructure.EFRepository;

namespace SupportTicket.Tests;

public class TicketServicesMoqTests
{
    [Fact]
    public void AddTicket_WhenValidData_CallsSaveExactlyOnce()
    {
        var repo = new Mock<IEFRepo>();
        var adoRepo = new Mock<IAdoRepo>();
        repo.Setup(x => x.GetTicketById(It.IsAny<int>()))
            .Returns(new Ticket
            {
                Id = 1,
                Title = "Network issue",
                Description = "Network is down",
                Priority = TicketPriority.High,
                Status = TicketStatus.Open,
                CustomerId = 1
            });

        var service = new TicketServices(repo.Object, adoRepo.Object);
        var ticket = new TicketCreateDto
        {
            Title = "Network issue",
            Description = "Network is down",
            Priority = TicketPriority.High,
            Status = TicketStatus.Open,
            CustomerId = 1
        };
        var result = service.AddTicket(ticket);
        Assert.Equal("Network issue", result.Title);
        repo.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public void AddTicket_WhenPriorityIsInvalid_DoesNotSaveTicket()
    {
        var repo = new Mock<IEFRepo>();
        var adoRepo = new Mock<IAdoRepo>();
        var service = new TicketServices(repo.Object, adoRepo.Object);
        var ticket = new TicketCreateDto
        {
            Title = "Printer issue",
            Description = "Printer is not working",
            Priority = (TicketPriority)999,
            Status = TicketStatus.Open,
            CustomerId = 1
        };
        Assert.Throws<ArgumentException>(() => service.AddTicket(ticket));
        repo.Verify(x => x.AddTicket(It.IsAny<Ticket>()), Times.Never);
        repo.Verify(x => x.Save(), Times.Never);
        repo.Verify(x => x.GetTicketById(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void DeleteCustomer_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var repo = new Mock<IEFRepo>();
        var adoRepo = new Mock<IAdoRepo>();
        repo.Setup(x => x.GetCustomerById(1))
            .Returns(new Customer
                Id = 1,
                Name = "Atarah",
                Email = "atarah@example.com",
                Phone = "98987654321"
            });

        repo.Setup(x => x.HasOpenTickets(1))
            .Returns(true);
        var service = new TicketServices(repo.Object, adoRepo.Object);
        var result = service.DeleteCustomer(1);
        Assert.Equal("HasOpenTickets", result);
        repo.Verify(x => x.DeleteCustomer(It.IsAny<int>()), Times.Never);
        repo.Verify(x => x.Save(), Times.Never);
    }

    [Fact]
    public void UpdateTicketStatus_WhenTicketIsClosed_ReturnsFalse()
    {
        var repo = new Mock<IEFRepo>();
        var adoRepo = new Mock<IAdoRepo>();
        adoRepo.Setup(x => x.UpdateTicketStatus(1, 2))
            .Returns(false);
        var service = new TicketServices(repo.Object, adoRepo.Object);
        var result = service.UpdateTicketStatus(1, 2);
        Assert.False(result);
        adoRepo.Verify(x => x.UpdateTicketStatus(1, 2), Times.Once);
    }

    [Fact]
    public void DeleteCustomer_WhenCustomerDoesNotExist_ReturnsFalse()
    {
        var repo = new Mock<IEFRepo>();
        var adoRepo = new Mock<IAdoRepo>();
        repo.Setup(x => x.GetCustomerById(99))
            .Returns((Customer?)null);
        var service = new TicketServices(repo.Object, adoRepo.Object);
        var result = service.DeleteCustomer(99);
        Assert.Equal("NotFound", result);
        repo.Verify(x => x.DeleteCustomer(It.IsAny<int>()), Times.Never);
        repo.Verify(x => x.Save(), Times.Never);
    }
}