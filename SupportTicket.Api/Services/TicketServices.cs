using SupportTicket.Api.DTOs;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Models;
using SupportTicket.Infrastructure.AdoRepository;
using SupportTicket.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Api.Services
{
    public class TicketServices:ITicketServices
    {
        private readonly IEFRepo _repo;
        private readonly IAdoRepo _adoRepo;
        public TicketServices(IEFRepo repo, IAdoRepo adoRepo)
        {
            _repo = repo;
            _adoRepo = adoRepo;
        }
        private TicketResponseDto MapToDto(Ticket ticket)
        {
            return new TicketResponseDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                CustomerId = ticket.CustomerId,
                AgentId = ticket.AgentId,
                ClosedAt = ticket.ClosedAt
            };
        }
        private CustomerResponseDto MapToDtoCustomer(Customer customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedAt = customer.CreatedAt
            };
        }
        public PagedResult<Ticket> GetAllTickets(int page, int pageSize)
        {
            return _repo.GetAllTickets(page, pageSize);
        }
        public TicketResponseDto? GetTicketById(int id)
        {
            var ticketById = _repo.GetTicketById(id);
            if(ticketById == null)
            {
                return null;
            }
            return MapToDto(ticketById);
        }
        public TicketResponseDto AddTicket(TicketCreateDto ticketCreateDto)
        {
            if (!Enum.IsDefined(typeof(TicketPriority), ticketCreateDto.Priority))
            {
                throw new ArgumentException("Invalid ticket priority.");
            }
            
            var ticket = new Ticket
            {
                Title = ticketCreateDto.Title,
                Description = ticketCreateDto.Description,
                Priority = ticketCreateDto.Priority,
                Status = ticketCreateDto.Status,
                CustomerId = ticketCreateDto.CustomerId,
                AgentId = ticketCreateDto.AgentId
            };
             _repo.AddTicket(ticket);
            _repo.Save();
            var saved = _repo.GetTicketById(ticket.Id);
            if (saved == null)
            {
                throw new InvalidOperationException("Ticket cannot be retrieved");
            }
            return MapToDto(saved);

        }
        public TicketResponseDto? UpdateTicket(TicketUpdateDto ticketUpdateDto, int id)
        {
            var ticket = _repo.GetTicketById(id);
            if(ticket == null)
            {
                return null;
            }
            if (!Enum.IsDefined(typeof(TicketPriority), ticketUpdateDto.Priority))
            {
                throw new ArgumentException("Invalid ticket priority.");
            }

            if (!Enum.IsDefined(typeof(TicketStatus), ticketUpdateDto.Status))
            {
                throw new ArgumentException("Invalid ticket status.");
            }
            ticket.Title = ticketUpdateDto.Title;
            ticket.Description = ticketUpdateDto.Description;
            ticket.Priority = ticketUpdateDto.Priority;
            ticket.Status = ticketUpdateDto.Status;
            ticket.CustomerId = ticketUpdateDto.CustomerId;
            ticket.AgentId = ticketUpdateDto.AgentId;
            _repo.UpdateTicket(ticket);
            _repo.Save();
            return MapToDto(ticket);
        }
        public bool DeleteTicket(int id)
        {
            var ticket = _repo.GetTicketById(id); 
            if(ticket == null)
            {
                return false;
            }
            _repo.DeleteTicket(ticket);
            _repo.UpdateTicket(ticket);
            _repo.Save();
            return true;

        }
        public IEnumerable<CustomerResponseDto> GetAllCustomers()
        {
            var customers = _repo.GetAllCustomers();

            return customers.Select(MapToDtoCustomer);
        }
        public CustomerResponseDto? GetCustomerById(int id)
        {
            var customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return null;
            }
            return MapToDtoCustomer(customer);
        }
        public CustomerResponseDto AddCustomer(CustomerCreateDto customerCreateDto)
        {
            var customer = new Customer
            {
                Name = customerCreateDto.Name,
                Email = customerCreateDto.Email,
                Phone = customerCreateDto.Phone
            };
            _repo.AddCustomer(customer);
            _repo.Save();
            var savedCustomer = _repo.GetCustomerById(customer.Id);
            if (savedCustomer == null)
            {
                throw new InvalidOperationException("Customer cannot be retrieved");
            }
            return MapToDtoCustomer(savedCustomer);
        }
        public CustomerResponseDto? UpdateCustomer(CustomerUpdateDto customerUpdateDto, int id)
        {
            var customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return null;
            }
            customer.Name = customerUpdateDto.Name;
            customer.Email = customerUpdateDto.Email;
            customer.Phone = customerUpdateDto.Phone;
            _repo.UpdateCustomer(customer);
            _repo.Save();
            return MapToDtoCustomer(customer);
        }
        public bool DeleteCustomer(int id)
        {
            var customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return false;
            }
            if (_repo.HasOpenTickets(id))
            {
                return false;
            }
            _repo.DeleteCustomer(id);
            _repo.Save();
            return true;
        }
        public void Save()
        {
            _repo.Save();
        }

        public IEnumerable<CustomerTicket> GetCustomerTicketDetails(int id)
        {
            return _adoRepo.GetCustomerTicketDetails(id);
        }

        public bool UpdateTicketStatus(int id, int newStatus)
        {
            return _adoRepo.UpdateTicketStatus(id, newStatus);
        }
    }
}
