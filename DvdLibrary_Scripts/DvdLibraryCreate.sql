-- Use master for DvdLibrary database creation
USE [master]
GO

-- Check if DvdLibrary database exists, if so drop it
IF EXISTS (SELECT * FROM sys.databases WHERE NAME = N'DvdLibrary')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'DvdLibrary';
	ALTER DATABASE DvdLibrary SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE DvdLibrary;
END

-- Create DvdLibrary database
CREATE DATABASE DvdLibrary
GO

-- Use DvdLibrary
USE DvdLibrary
GO

-- Dvds database table
CREATE TABLE Dvds (
	id INT PRIMARY KEY IDENTITY (1, 1),
	title VARCHAR(50) NOT NULL,
	releaseYear INT NOT NULL,
	director VARCHAR(30) NULL,
	rating VARCHAR(10) NULL,
	notes VARCHAR(200) NULL,
)
GO