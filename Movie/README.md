# Movie API

Created using ASP.NET Core, OpenAPI, and Docker

## Setup

### Installing Dev Certs

Run following commands at root of the app folder

```
dotnet dev-certs https --clean
dotnet dev-certs https -ep .aspnet\https\aspnetapp.pfx -p sanguine
dotnet dev-certs https --trust
```

### Running the container

```
docker-compose -f docker-compose.yml up -d
```

### Testing the API

With the help of Swagger, you should be able to navigate API from https://localhost:44369/swagger/index.html

## find

Returns list of movies for provided search criteria which includes:
* Year of Release
* Title (full or partial)
* Genres

## top

Returns list of top rated movies. If userId is provided then the list is ordered based on user's rating

## put

Updates rating to for given user and title