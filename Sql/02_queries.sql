USE SupportTicketDb;
GO

SELECT C.Name AS CustomerName , A.Name AS AgentName , T.Status From Tickets T INNER JOIN Customers C ON T.CustomerId = C.Id Left JOIN Agents A ON T.AgentId = A.Id
WHERE T.Status = 0;

SELECT A.Id ,A.Name , COUNT(T.Id) AS TicketCount FROM Tickets T RIGHT JOIN Agents A ON T.AgentId = A.Id GROUP BY A.Id , A.Name ;

SELECT Id , Title , Description , CreatedAt, ClosedAt From Tickets WHERE Title LIKE '%Login%';

SELECT TOP 3 A.Id , A.Name As AgentName , Count(T.Id) AS ResolvedTicketCount  FROM Agents A INNER JOIN Tickets T ON A.Id = T.AgentId
WHERE T.Status = 2 GROUP BY A.Id,A.Name ORDER BY Count(T.Id) DESC;

SELECT C.Id AS CustomerId , C.Name AS CustomerName , Count(T.Id) As OpenTicketCount FROM Customers C INNER JOIN Tickets T ON C.Id = T.CustomerId
WHERE T.Status = 0  GROUP BY C.Id , C.Name Having Count(T.Id) > 2;

BEGIN TRANSACTION
IF EXISTS (SELECT 1 FROM Agents 
WHERE Id = 2 AND IsActive = 1
)
BEGIN
	UPDATE Tickets SET AgentId = 2
	WHERE Id = 5;
	COMMIT TRANSACTION;
	PRINT 'Ticket is assigned.';
END
ELSE
BEGIN
	ROLLBACK TRANSACTION 
	PRINT 'Ticket cannot be assigned as Agent is Not-Active';
END

