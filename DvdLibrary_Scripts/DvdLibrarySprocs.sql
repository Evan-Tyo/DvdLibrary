-- Use DvdLibrary
USE DvdLibrary
GO

-- Dvd select all stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectAll')
      DROP PROCEDURE usp_DvdSelectAll
GO

CREATE PROCEDURE usp_DvdSelectAll
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	ORDER BY id
GO

-- Dvd select by id stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectById')
      DROP PROCEDURE usp_DvdSelectById
GO

CREATE PROCEDURE usp_DvdSelectById (
	@Id int
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE Dvds.id = @Id
	ORDER BY id
GO

-- Dvd select by title stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectByTitle')
      DROP PROCEDURE usp_DvdSelectByTitle
GO

CREATE PROCEDURE usp_DvdSelectByTitle (
	@Title VARCHAR(50)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE Dvds.title LIKE @Title + '%'
	ORDER BY id
GO

-- Dvd select by release year stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectByYear')
      DROP PROCEDURE usp_DvdSelectByYear
GO

CREATE PROCEDURE usp_DvdSelectByYear (
	@ReleaseYear int
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE Dvds.releaseYear = @ReleaseYear
	ORDER BY id
GO

-- Dvd select by director stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectByDirector')
      DROP PROCEDURE usp_DvdSelectByDirector
GO

CREATE PROCEDURE usp_DvdSelectByDirector (
	@Director VARCHAR(30)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE Dvds.director LIKE '%' + @Director + '%'
	ORDER BY id
GO

-- Dvd select by rating stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdSelectByRating')
      DROP PROCEDURE usp_DvdSelectByRating
GO

CREATE PROCEDURE usp_DvdSelectByRating (
	@Rating VARCHAR(10)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE Dvds.rating = @Rating
	ORDER BY id
GO

-- Dvd create by insert stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'usp_DvdAdd')
		DROP PROCEDURE usp_DvdAdd
GO

CREATE PROCEDURE usp_DvdAdd (
	@Id int output,
	@Title VARCHAR(50),
	@ReleaseYear int,
	@Director VARCHAR(30),
	@Rating VARCHAR(10),
	@Notes VARCHAR(200)
)
AS
	INSERT INTO Dvds (title, releaseYear, director, rating, notes)
	VALUES (@Title, @ReleaseYear, @Director, @Rating, @Notes)

	SET @Id = SCOPE_IDENTITY()
GO

-- Dvd edit by update stored procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdUpdate')
      DROP PROCEDURE usp_DvdUpdate
GO

CREATE PROCEDURE usp_DvdUpdate (
	@Id int output,
	@Title VARCHAR(50),
	@ReleaseYear int,
	@Director VARCHAR(30),
	@Rating VARCHAR(10),
	@Notes VARCHAR(200)
)
AS
	UPDATE Dvds
		SET title = @Title,
			releaseYear = @ReleaseYear,
			director = @Director,
			rating = @Rating,
			notes = @Notes
	WHERE id = @Id
GO

-- Dvd delete store procedure
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'usp_DvdDelete')
      DROP PROCEDURE usp_DvdDelete
GO

CREATE PROCEDURE usp_DvdDelete (
	@Id int
)
AS
	DELETE FROM Dvds
	WHERE id = @Id
GO