namespace Movie.Engine.DataAccess
{
    class MovieRepositorySql
    {
        public const string FindMovies = @"
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

        public const string GetTopRatedMovies = @"
DECLARE @tblMovie TABLE (
  Id BIGINT NOT NULL,
  Title NVARCHAR(100) NOT NULL,
  ReleaseYear INT NOT NULL,
  RunningTime TIME NOT NULL,
  Genres NVARCHAR(400) NOT NULL
)

; WITH rankedMovie AS (
  SELECT
    umr.MovieId,
    AVG(CAST(Score AS FLOAT)) AS AvgScore
  FROM dbo.tblUserMovieRating umr
  WHERE
    (
      @userId IS NULL
      OR umr.UserId = @userId
    )
  GROUP BY
    umr.MovieId
)
INSERT INTO @tblMovie (
  Id,
  Title,
  ReleaseYear,
  RunningTime,
  Genres
)
SELECT TOP(@count)
  m.[Id],
  m.[Title],
  m.[ReleaseYear],
  m.[RunningTime],
  fnGenre.[Genres]
FROM rankedMovie rm
JOIN dbo.tblMovie m ON
  m.Id = rm.MovieId
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
ORDER BY
  AvgScore DESC,
  m.[Title] ASC

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
  m.Id = r.MovieId";

        public const string UpsertRatings = @"
MERGE dbo.tblUserMovieRating AS t
USING (SELECT @userId UserId, @movieId MovieId) AS s
  ON s.UserId = t.UserId
  AND s.MovieId = t.MovieId
WHEN MATCHED THEN UPDATE
  SET Score = @score, UpdatedOn = GETDATE()
WHEN NOT MATCHED THEN
  INSERT (UserId, MovieId, Score)
  VALUES (@userId, @movieId, @score);";

        public const string GetMovie = @"
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
  m.[Id] = @movieId";

        public const string GetUser = @"
SELECT
  u.[Id],
  u.[Username]
FROM dbo.tblUser u
WHERE
  u.[Id] = @userId";
    }
}
