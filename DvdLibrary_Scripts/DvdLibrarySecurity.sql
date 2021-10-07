-- Use master for DvdLibrary database creation
USE [master]
GO

-- Check if login exists and drop
IF EXISTS (SELECT * FROM master.sys.server_principals WHERE NAME = 'DvdLibraryApp')
	DROP LOGIN DvdLibraryApp
GO

-- Create login and password
CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

-- Use DvdLibrary
USE DvdLibrary
GO

-- Check if user exists and drop
IF EXISTS (SELECT * FROM sys.database_principals WHERE NAME = 'DvdLibraryApp')
	DROP USER DvdLibraryApp
GO

-- Create dvd library database account
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

-- Grant execute on all stored procedures
GRANT EXECUTE ON usp_DvdSelectAll TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdSelectById TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdSelectByTitle TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdSelectByYear TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdSelectByDirector TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdSelectByRating TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdAdd TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdUpdate TO DvdLibraryApp
GRANT EXECUTE ON usp_DvdDelete TO DvdLibraryApp
GO

-- Grant select, insert, update, and delete on all used tables
GRANT SELECT ON Dvds TO DvdLibraryApp
GRANT INSERT ON Dvds TO DvdLibraryApp
GRANT UPDATE ON Dvds TO DvdLibraryApp
GRANT DELETE ON Dvds TO DvdLibraryApp
GO