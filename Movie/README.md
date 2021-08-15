# Movie API

Created using ASP.NET Core & OpenAPI

## find

Returns list of movies for provided search criteria which includes:
* Year of Release
* Title (full or partial)
* Genres

## top

Returns list of top rated movies. If userId is provided then the list is ordered based on user's rating

## put

Updates rating to for given user and title