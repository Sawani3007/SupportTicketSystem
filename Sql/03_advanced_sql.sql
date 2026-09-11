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

CREATE PROCEDURE GivenCustomerTicketDetails 
@CustomerId int
AS BEGIN 
SELECT Id , Title,Priority , Status From Tickets
WHERE CustomerId = @CustomerId
END;
GO


CREATE PROCEDURE UpdatingTicketStatus
    @TicketId int,
    @NewStatus int
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Tickets
        WHERE Id = @TicketId
          AND Status = 3
    )
    BEGIN
        PRINT 'Ticket is already Closed. Status cannot be updated';
        RETURN;
    END

    UPDATE Tickets
    SET Status = @NewStatus
    WHERE Id = @TicketId;
END
GO

