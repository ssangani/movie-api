-- Create DB & Schema

CREATE DATABASE MovieDb

USE MovieDb

CREATE TABLE dbo.tblplGenre (
  Id INT NOT NULL,
  Name NVARCHAR(50) NOT NULL
)

CREATE TABLE dbo.tblMovie (
  Id BIGINT NOT NULL IDENTITY(1, 1),
  Title NVARCHAR(100) NOT NULL,
  ReleaseYear INT NOT NULL,
  RunningTime TIME NOT NULL
)

CREATE TABLE dbo.tbljoinMovieGenre (
  Id BIGINT NOT NULL IDENTITY(1, 1),
  MovieId BIGINT NOT NULL,
  GenreId INT NOT NULL
)

CREATE TABLE dbo.tblUser (
  Id BIGINT NOT NULL IDENTITY(1, 1),
  Username NVARCHAR(50) NOT NULL
)

CREATE TABLE dbo.tblUserMovieRating (
  Id BIGINT NOT NULL IDENTITY(1, 1),
  UserId BIGINT NOT NULL,
  MovieId BIGINT NOT NULL,
  Score TINYINT NOT NULL,
  AddedOn DATETIME NOT NULL CONSTRAINT DF_tblUserMovieRating_AddedOn DEFAULT GETDATE(),
  UpdatedOn DATETIME NOT NULL CONSTRAINT DF_tblUserMovieRating_UpdatedOn DEFAULT GETDATE()
)

-- Seed Data
DECLARE @actionId INT = 1,
        @adventureId INT = 2,
        @biographyId INT = 3,
        @comedyId INT = 4,
        @crimeId INT = 5,
        @dramaId INT = 6,
        @familyId INT = 7,
        @fantasyId INT = 8,
        @musicId INT = 9,
        @mysteryId INT = 10,
        @noirId INT = 11,
        @romanceId INT = 12,
        @thrillerId INT = 13,
        @warId INT = 14,
        @movieId BIGINT = NULL

INSERT INTO dbo.tblplGenre (
  Id,
  Name
)
VALUES
  (@actionId, 'Action'),
  (@adventureId, 'Adventure'),
  (@biographyId, 'Biography'),
  (@comedyId, 'Comedy'),
  (@crimeId, 'Crime'),
  (@dramaId, 'Drama'),
  (@familyId, 'Family'),
  (@fantasyId, 'Fantasy'),
  (@musicId, 'Music'),
  (@mysteryId, 'Mystery'),
  (@noirId, 'Noir'),
  (@romanceId, 'Romance'),
  (@thrillerId, 'Thriller'),
  (@warId, 'War')

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('My Man Godfrey', 1936, '1:34:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('It Happened One Night', 1934, '1:45:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Apartment', 1960, '2:05:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('How to Steal a Million', 1966, '2:03:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @crimeId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('To Catch a Thief', 195546, '1:34:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @mysteryId),
  (@movieId, @thrillerId),
  (@movieId, @romanceId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('It''s a Wonderful Life', 1946, '2:10:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @dramaId),
  (@movieId, @familyId),
  (@movieId, @fantasyId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Mr. Deeds Goes to Town', 1936, '1:55:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Mr. Smith Goes to Washington', 1939, '2:09:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Shop Around the Corner', 1940, '1:39:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Doctor Zhivago', 1965, '3:17:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @dramaId),
  (@movieId, @romanceId),
  (@movieId, @warId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Lawrence of Arabia', 1962, '3:48:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @adventureId),
  (@movieId, @biographyId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('You Can''t Take It With You', 1938, '2:06:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Awful Truth', 1937, '1:30:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Breakfast At Tiffany''s', 1936, '1:55:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @dramaId),
  (@movieId, @romanceId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Artist', 2011, '1:40:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @comedyId),
  (@movieId, @romanceId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Murder on the Orient Express', 1974, '2:08:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @crimeId),
  (@movieId, @dramaId),
  (@movieId, @mysteryId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Treasure of the Sierra Madre', 1948, '2:06:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @adventureId),
  (@movieId, @dramaId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('The Big Sleep', 1946, '1:54:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @crimeId),
  (@movieId, @noirId),
  (@movieId, @mysteryId)

INSERT INTO dbo.tblMovie (
  Title,
  ReleaseYear,
  RunningTime
)
VALUES
  ('Casablanca', 1942, '1:42:00')

SET @movieId = SCOPE_IDENTITY()

INSERT INTO dbo.tbljoinMovieGenre (
  MovieId,
  GenreId
)
VALUES
  (@movieId, @dramaId),
  (@movieId, @romanceId),
  (@movieId, @warId)
  
/*
SELECT * FROM dbo.tblplGenre
SELECT * FROM dbo.tblMovie
SELECT m.*, g.genres
FROM dbo.tblMovie m
OUTER APPLY (
  SELECT
    STUFF(
      (
        SELECT
          ',' + pg.[Name]
        FROM dbo.tbljoinMovieGenre mg
        JOIN dbo.tblplGenre pg ON
          pg.Id = mg.GenreId
        WHERE
          mg.MovieId = m.Id
        FOR XML PATH('')
      ),
      1,
      1,
      ''
    ) AS genres
) AS g
*/

/*
DROP TABLE tblplGenre
DROP TABLE tblMovie
DROP TABLE tbljoinMovieGenre
DROP TABLE tblUser
DROP TABLE tblUserMovieRating
*/
