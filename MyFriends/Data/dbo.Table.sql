CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL,
	[Patronymic] NVARCHAR(50) NOT NULL,
	[Year] INT NOT NULL,
	[Month] INT NOT NULL,
	[Day] INT NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	[NumberCode] INT NULL,
	[Number] INT NULL
)
