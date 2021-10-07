-- Use DvdLibrary
USE DvdLibrary
GO

-- Delete all the rows from the table
TRUNCATE TABLE Dvds
GO

-- Reseed the table after truncate
DBCC CHECKIDENT ('Dvds', RESEED, 1);
GO

-- Add sample data to the Dvd Library
SET IDENTITY_INSERT Dvds ON;
	INSERT INTO Dvds (id, title, releaseYear, director, rating, notes) VALUES
		(1, 'A Great Tale', 2015, 'Sam Jones', 'PG', 'This really is a great tale!'),
		(2, 'A Good Tale', 2012, 'Joe Smith', 'PG-13', 'This is a good tale!'),
		(3, 'A Super Tale', 2015, 'Sam Jones', 'G', 'A great remake!'),
		(4, 'A Super Tale', 1985, 'Joe Smith', 'PG', 'The original!'),
		(5, 'A Wonderful Tale', 2011, 'Sam Smith', 'R', 'Wonderful, just wonderful!'),
		(6, 'A Great Tale', 2015, 'Sam Jones', 'PG', 'This really is a great tale!');
SET IDENTITY_INSERT Dvds OFF;