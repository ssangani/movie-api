namespace Movie.Engine.DataAccess
{
    class MovieDaoQuery
    {
        public const string GetMoviesSql = @"
DECLARE @tblGenre TABLE (
  Id INT NOT NULL
)

DECLARE @tblMovie TABLE (
  Id BIGINT NOT NULL,
  Title NVARCHAR(100) NOT NULL,
  ReleaseYear INT NOT NULL,
  RunningTime TIME NOT NULL,
  Genres NVARCHAR(400) NOT NULL
)

INSERT INTO @tblGenre (Id)
SELECT value FROM STRING_SPLIT(@genres, ',')

INSERT INTO @tblMovie (
  Id,
  Title,
  ReleaseYear,
  RunningTime,
  Genres
)
SELECT
  m.[Id],
  m.[Title],
  m.[ReleaseYear],
  m.[RunningTime],
  fnGenre.[Genres]
FROM dbo.tblMovie m
OUTER APPLY (
  SELECT
    STUFF (
      (
        SELECT
          ',' + CAST(g.[Id] AS NVARCHAR(20))
        FROM dbo.tbljoinMovieGenre mg
        JOIN dbo.tblplGenre g ON
          g.[Id] = mg.[GenreId]
        WHERE
          mg.[MovieId] = m.[Id]
        FOR XML PATH('')
      ),
      1,
      1,
      ''
    ) AS Genres
) AS fnGenre
WHERE
  (
    @titleLike IS NULL
    OR m.[Title] LIKE '%' + @titleLike + '%'
  )
  AND (
    @yearOfRelease IS NULL
    OR m.[ReleaseYear] = @yearOfRelease
  )
  AND (
    @genres IS NULL
    OR EXISTS (
      SELECT 1
      FROM dbo.tbljoinMovieGenre mg
      JOIN @tblGenre ug ON
        ug.[Id] = mg.[GenreId]
      WHERE
        mg.[MovieId] = m.[Id]
    )
  )

SELECT
  Id,
  Title,
  ReleaseYear,
  RunningTime,
  Genres
FROM @tblMovie

SELECT
  r.Id,
  r.UserId,
  r.MovieId,
  r.Score
FROM dbo.tblUserMovieRating r
JOIN @tblMovie m ON
  m.Id = r.MovieId
";
    }
}
