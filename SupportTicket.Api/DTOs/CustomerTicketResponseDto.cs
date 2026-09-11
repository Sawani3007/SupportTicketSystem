namespace SupportTicket.Api.DTOs
{
    public class CustomerTicketResponseDto
    {
        public int TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Priority { get; set; }
        public int Status { get; set; }
    }
}