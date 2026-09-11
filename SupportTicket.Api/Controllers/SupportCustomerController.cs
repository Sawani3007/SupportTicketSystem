using Microsoft.AspNetCore.Mvc;
using SupportTicket.Api.Services;
using SupportTicket.Api.DTOs;

namespace SupportTicket.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ITicketServices _service;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ITicketServices service,
            ILogger<CustomerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            _logger.LogInformation("Fetching all customers.");

            var customers = _service.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            _logger.LogInformation("Fetching customer with ID {CustomerId}.", id);

            var customer = _service.GetCustomerById(id);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} was not found.", id);
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerCreateDto customerCreateDto)
        {
            _logger.LogInformation("Creating a new customer.");

            var customer = _service.AddCustomer(customerCreateDto);

            _logger.LogInformation("Customer created successfully.");

            return CreatedAtAction(
    nameof(GetCustomerById),
    new { id = customer.Id },
    customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(
            int id,
            CustomerUpdateDto customerUpdateDto)
        {
            _logger.LogInformation("Updating customer with ID {CustomerId}.", id);

            var customer = _service.UpdateCustomer(customerUpdateDto, id);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} was not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Customer with ID {CustomerId} updated successfully.", id);

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting customer with ID {CustomerId}.", id);
            var result = _service.DeleteCustomer(id);
            if (result == "NotFound")
            {
                _logger.LogWarning(
                    "Customer with ID {CustomerId} was not found.",
                    id);

                return NotFound();
            }

            if (result == "HasOpenTickets")
            {
                _logger.LogWarning(
                    "Customer with ID {CustomerId} cannot be deleted because they have open tickets.",
                    id);
                return BadRequest(
                    "Customer cannot be deleted because they have open tickets.");
            }
            _logger.LogInformation(
                "Customer with ID {CustomerId} deleted successfully.",
                id);
            return NoContent();
        }
    }
}