using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SupportTicket.Core.Models;
using SupportTicket.Core.DTOs;

namespace SupportTicket.Application.Services
{
    public interface ITicketServices
    {
        public IEnumerable<TicketResponseDto> GetAllTickets();
        public TicketResponseDto? GetTicketById(int id);
        public void AddTicket(TicketCreateDto ticketCreateDto);
        public TicketResponseDto? UpdateTicket(TicketUpdateDto ticketUpdateDto, int id);
        public bool DeleteTicket(int id);
        public IEnumerable<CustomerResponseDto> GetAllCustomers();
        public CustomerResponseDto? GetCustomerById(int id);
        public CustomerResponseDto AddCustomer(CustomerCreateDto customerCreateDto);
        public CustomerResponseDto? UpdateCustomer(CustomerUpdateDto customerUpdateDto, int id);
        public void DeleteCustomer(int id);
        public void Save();
    }
}
