# Movie API

Created using ASP.NET Core, OpenAPI, and Docker

## Setup

### Sparse Mode

1. This approach doesn't require Docker. All you need to do is within `DependencyInjectionExtensions.cs` replace the line below with line after:

```
services.AddTransient<IMovieRepository, MovieRepository>();
```

```
services.AddSingleton<IMovieRepository, MovieRepositoryStub>();
```

This will setup the application to use an stubbed out in-memory repo (the same one from integration tests). After that change you can run the application locally.

2. After setup completes you should open https://localhost:44469/swagger/index.html in local browser and it will help you navigate the API.

### Container Mode

1. If you don't have a local dev certificate you can use dotnet CLI to generate one for you with following commands -

```
dotnet dev-certs https -ep ~\.aspnet\https\aspnetapp.pfx -p sanguine
dotnet dev-certs https --trust
```

Or if you already have a local cert, you can modify the `docker-compose.yml` to point to that certificate along with it's password.

2. Once you have certs part figured out you can run the docker-compose command (in app directory) to bring up the ecosystem.

```
docker-compose -f docker-compose.yml up -d
```

3. Once the containers are up, you should open https://localhost:6060/swagger/index.html in local browser and it will you navigate the API.

## API

### find

Returns list of movies for provided search criteria which includes:
* Year of Release
* Title (full or partial)
* Genres

### top

Returns list of top rated movies. If userId is provided then the list is ordered based on user's rating

### put

Updates rating to for given user and title