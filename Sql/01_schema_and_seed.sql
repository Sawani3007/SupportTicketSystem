CREATE DATABASE SupportTicketDb;
GO

USE SupportTicketDb;
GO

CREATE TABLE Customers
(
    Id int Identity(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Email VARCHAR(30) NOT NULL UNIQUE,
    Phone VARCHAR(12) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Agents
(
    Id int Identity(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Email VARCHAR(30) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    Department VARCHAR(20) NOT NULL
);
GO

CREATE TABLE Tickets
(
    Id int Identity(1,1) PRIMARY KEY,
    Title VARCHAR(50) NOT NULL,
    Description VARCHAR(200) NOT NULL,
    Priority int NOT NULL,
    Status int NOT NULL,
    CustomerId int NOT NULL,
    AgentId int NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    ClosedAt DATETIME2 NULL,
    FOREIGN KEY(CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY(AgentId) REFERENCES Agents(Id)
);
GO

CREATE TABLE TicketNotes
(
    Id int Identity(1,1) PRIMARY KEY,
    TicketId int NOT NULL,
    NoteText VARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY(TicketId) REFERENCES Tickets(Id)
);
GO

INSERT INTO Customers (Name, Email, Phone)
VALUES
('Rahul Sharma', 'rahul@gmail.com', '9876543210'),
('Priya Singh', 'priya@gmail.com', '9876543211'),
('Aman Verma', 'aman@gmail.com', '9876543212');
GO

INSERT INTO Agents (Name, Email, Department, IsActive)
VALUES
('Siya Gomez', 'siya@support.com', 'Technical Support', 1),
('Ishika Saxena', 'Ishika@support.com', 'Technical Support', 1),
('Bhawana Srivastava', 'bhawana@support.com', 'Technical Support', 0);
GO

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
GO

INSERT INTO TicketNotes
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
    5,
    'Crash issue was identified and fixed.',
    '2026-09-04 10:00:00'
);
GO

SELECT * FROM Customers;
SELECT * FROM Agents;
SELECT * FROM Tickets;
SELECT * FROM TicketNotes;