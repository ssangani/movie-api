namespace Movie.Engine.DataAccess
{
    class MovieDaoQuery
    {
        public const string GetMoviesSql = @"
DECLARE @tblGenre TABLE (Id INT NOT NULL)

INSERT INTO @tblGenre (Id)
SELECT value FROM STRING_SPLIT(@genres, ',')

SELECT
  m.[Id],
  m.[Title] AS TitleName,
  m.[ReleaseYear],
  m.[RunningTime],
  fnGenre.Genres
FROM dbo.tblMovie m
OUTER APPLY (
  SELECT
    STUFF (
      (
        SELECT
          ',' + g.[Name]
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
  )";
    }
}
