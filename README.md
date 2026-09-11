# Support Ticket System

A simple support ticket management system built using ASP.NET Core Web API, Entity Framework Core, ADO.NET, SQL Server, and a basic HTML/CSS/JavaScript frontend.

The system allows users to manage customers and support tickets, search and filter tickets, update ticket status, and view tickets belonging to a customer.

## Project Structure

- `SupportTicket.Api` - ASP.NET Core Web API, controllers, DTOs, services and middleware
- `SupportTicket.Core` - Models, enums and common application logic
- `SupportTicket.Infrastructure` - Entity Framework Core and ADO.NET repositories
- `SupportTicket.Tests` - Unit tests using xUnit and Moq
- `SupportTicket.Console` - Console application for LINQ and asynchronous JSON/API tasks
- `Frontend` - HTML, CSS and JavaScript files
- `Legacy` - Legacy configuration files
- `Sql` - SQL Server scripts, views and stored procedures
- `Debug` - Debugging screenshots

## Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- ADO.NET
- SQL Server
- LINQ
- HTML
- CSS
- JavaScript
- xUnit
- Moq
- Swagger

## Database Setup

1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Open the SQL script from the `Sql` folder.
4. Run the script to create the `SupportTicketDb` database, tables and sample data.
5. Check the connection string in:

   `SupportTicket.Api/appsettings.json`

6. The application uses the connection string named:

   `DefaultString`

The database contains Customers, Agents, Tickets and TicketNotes tables along with sample records.

## Run the API

From the solution folder, run:

```bash
dotnet run --project SupportTicket.Api