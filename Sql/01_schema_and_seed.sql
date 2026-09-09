CREATE DATABASE SupportTicketDb;
GO
USE SupportTicketDb;
GO

CREATE TABLE Customers(Id int Identity(1,1) PRIMARY KEY , Name VARCHAR(50) Not Null , Email VARCHAR(30) Not Null Unique , Phone Varchar(12) Not Null,
CreatedAt DATETIME2 Not Null DEFAULT GETDATE());
GO

CREATE TABLE Agents(Id int Identity(1,1) PRIMARY KEY , Name VARCHAR(50) Not Null , Email VARCHAR(30) Not Null Unique , IsActive BIT Not Null DEFAULT 1
, CreatedAt DATETIME2 Not Null DEFAULT GETDATE());
GO
ALTER Table Agents Add Department Varchar(20) Not Null;
GO

CREATE TABLE Tickets(Id int Identity(1,2) Primary Key , Title VARCHAR(50) Not Null , Description VARCHAR(50) Not Null , Priority int Not Null 
, Status int Not Null , CustomerId int , AgentId int Null , CreatedAt DATETIME2 Not Null DEFAULT GETDATE() , ClosedAt DATETIME2 Null ,
FOREIGN KEY(CustomerId) REFERENCES Customers(Id) ,
FOREIGN KEY(AgentId) REFERENCES Agents(Id));
GO

ALTER TABLE Tickets ALTER COLUMN Description VARCHAR(200);

CREATE TABLE TicketsNotes(Id int Identity(1,1) PRIMARY KEY , TicketId int , NoteText VARCHAR(100) NOT NULL , CreatedAt DATETIME2 Not Null DEFAULT GETDATE()
,FOREIGN KEY(TicketId) REFERENCES Tickets(Id));
GO

INSERT INTO Customers (Name, Email, Phone)
VALUES
('Rahul Sharma', 'rahul@gmail.com', '9876543210'),
('Priya Singh', 'priya@gmail.com', '9876543211'),
('Aman Verma', 'aman@gmail.com', '9876543212');


INSERT INTO Agents (Name, Email, Department, IsActive)
VALUES
('Siya Gomez', 'siya@support.com', 'Technical Support', 1),
('Ishika Saxena', 'Ishika@support.com', 'Technical Support', 1),
('Bhawana Srivastava', 'bhawana@support.com', 'Technical Support', 0);

INSERT INTO Tickets
(
    Title,
    Description,
    Priority,
    Status,
    CustomerId,
    AgentId,
    CreatedAt,
    ClosedAt
)
VALUES
(
    'Login issue',
    'Customer is unable to login to the application.',
    2,
    0,
    1,
    1,
    '2026-09-01 09:00:00',
    NULL
),

(
    'Login failed after password reset',
    'Customer still cannot login after resetting password.',
    2,
    0,
    1,
    NULL,
    '2026-09-01 10:30:00',
    NULL
),

(
    'Profile update not working',
    'Customer cannot update profile information.',
    1,
    0,
    1,
    2,
    '2026-09-02 11:00:00',
    NULL
),

(
    'Email notification delay',
    'Customer is receiving notifications late.',
    0,
    0,
    2,
    1,
    '2026-09-02 12:00:00',
    NULL
),

(
    'Payment processing error',
    'Payment fails while completing the transaction.',
    2,
    1,
    2,
    2,
    '2026-09-03 09:30:00',
    NULL
),

(
    'Application crash',
    'Application crashes when opening the dashboard.',
    2,
    2,
    3,
    1,
    '2026-09-03 14:00:00',
    NULL
),

(
    'Password reset request',
    'Customer requested help resetting password.',
    1,
    3,
    3,
    2,
    '2026-09-04 10:00:00',
    '2026-09-05 16:00:00'
),

(
    'Report download issue',
    'Customer could not download the monthly report.',
    0,
    2,
    2,
    1,
    '2026-09-04 15:00:00',
    NULL
);

INSERT INTO TicketsNotes
(
    TicketId,
    NoteText,
    CreatedAt
)
VALUES
(
    2,
    'Customer reported that login fails with a valid password.',
    '2026-09-01 09:30:00'
),
(
    4,
    'Agent is investigating the authentication issue.',
    '2026-09-01 11:00:00'
),
(
    6,
    'Customer provided a screenshot of the profile update error.',
    '2026-09-02 13:00:00'
),
(
    8,
    'Payment team is checking the transaction logs.',
    '2026-09-03 11:00:00'
),
(
    10,
    'Crash issue was identified and fixed.',
    '2026-09-04 10:00:00'
);
SELECT * FROM Customers;
SELECT * FROM Agents;
SELECT * FROM TicketsNotes;
SELECT * FROM Tickets;
