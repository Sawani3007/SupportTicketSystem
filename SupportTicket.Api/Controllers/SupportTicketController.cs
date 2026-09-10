using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.Api.DTOs;
using SupportTicket.Api.Services;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;

namespace SupportTicket.Api.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class SupportTicketController : ControllerBase
    {
        private readonly ILogger<SupportTicketController> _logger;
        private readonly ITicketServices _service;
        private readonly IConfiguration _configuration;
        public SupportTicketController(ILogger<SupportTicketController> logger, ITicketServices service, IConfiguration configuration)
        {
            _logger = logger;
            _service = service;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAllTickets(int page = 1,int? pageSize = null,
    string? search = null,TicketStatus? status = null,TicketPriority? priority = null)
        {
            int defaultPageSize = _configuration.GetValue<int>("DefaultPageSize");
            int actualPageSize = pageSize ?? defaultPageSize;

            if (page < 1 || actualPageSize < 1)
            {
                return BadRequest("Page and pagesize must be greater than 0");
            }

            var result = _service.GetAllTickets(
                page,
                actualPageSize,
                search,
                status,
                priority);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult<TicketResponseDto> GetTicketById(int id)
        {
            var result = _service.GetTicketById(id);
            _logger.LogInformation("Fetching tickets information from Id {Id}", id);
            if (result == null)
            {
                return NotFound("Ticket with Id not found.");
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddTicket(TicketCreateDto dto)
        {
            var ticket = _service.AddTicket(dto);
            _logger.LogInformation("Adding ticket:");

            return CreatedAtAction(
                nameof(GetTicketById),
                new { id = ticket.Id },
                ticket);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTicket(TicketUpdateDto dto,int id)
        {
            var ticket = _service.UpdateTicket(dto, id);
            _logger.LogInformation("Updating the ticket with Id{Id}", id);
            return Ok(ticket);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var deleted = _service.DeleteTicket(id);
            _logger.LogInformation("Deleting ticket with Id: {Id}", id);

            if (!deleted)
                return NotFound("Ticket not found.");

            return NoContent();
        }
        [HttpGet("by-customer/{customerId}")]
        public IActionResult GetCustomerTickets(int customerId)
        {
            _logger.LogInformation(
                "Fetching tickets for customer {CustomerId}.",
                customerId);

            var result = _service.GetCustomerTicketDetails(customerId);

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPatch("{id}/status")]
        public IActionResult UpdateTicketStatus(int id, int newStatus)
        {
            _logger.LogInformation(
                "Updating status for ticket {TicketId}.",
                id);

            var result = _service.UpdateTicketStatus(id, newStatus);

            if (!result)
            {
                return BadRequest("Ticket status could not be updated.");
            }

            return NoContent();
        }

    }
}