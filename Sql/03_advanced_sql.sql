/*1. A view showing ticket id, title, priority, status, customer name, agent name, and created date*/

USE SupportTicketDb;
GO

CREATE VIEW TicketDetails
AS 
SELECT t.Id As TicketId ,
t.Title , t.Priority ,  t.Status,
c.Name As CustomerName, a.Name As AgentName , t.CreatedAt From Tickets t Inner Join 
Customers c On t.CustomerId = c.Id Left Join Agents a ON t.AgentId = a.Id;
GO

SELECT * FROM TicketDetails;
GO
/*2. A stored procedure that returns tickets for a given customer id*/

CREATE PROCEDURE GivenCustomerTicketDetails 
@CustomerId int
AS BEGIN 
SELECT Id , Title,Priority , Status From Tickets
WHERE CustomerId = @CustomerId
END;
GO

EXEC GivenCustomerTicketDetails 3;
GO

/*A stored procedure that updates ticket status and rejects the change if the ticket is already Closed. Use parameters only.*/

CREATE PROCEDURE UpdatingTicketStatus
@TicketId int,
@NewStatus int
As BEGIN
BEGIN TRANSACTION
IF EXISTS (SELECT 1 FROM Tickets WHERE Id = @TicketId AND Status = 3)
BEGIN 
	ROLLBACK TRANSACTION
	PRINT 'Ticket is already Closed. Status cannot be updated';
END
ELSE 
BEGIN 
	UPDATE Tickets SET Status = @NewStatus WHERE Id = @TicketId ;
	PRINT 'Ticket Status is Updated';
	COMMIT TRANSACTION;
END
END
GO

EXEC UpdatingTicketStatus @TicketId = 5 , @NewStatus = 2;