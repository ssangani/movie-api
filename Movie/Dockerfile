# Builder image using dotnet v5 sdk
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Define arguments
ARG dotnet_build_log_level=q

# Copy app files and build
COPY *.sln ./
COPY */*.csproj ./
RUN for project in /src/*.csproj; do mkdir ${project%.*}; mv $project ${project%.*}; done
RUN dotnet restore -r linux-x64 --verbosity ${dotnet_build_log_level} Movie.Api

# Copy DLLs and publish
COPY . ./
WORKDIR /src/Movie.Api
RUN dotnet publish -c Release -r linux-x64 -o /app --verbosity ${dotnet_build_log_level}

# Setup runtime container
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=build /app ./
COPY ./sql-scripts/wait-for-it.sh .
EXPOSE 6060
ENTRYPOINT [ "dotnet", "Movie.Api.dll" ]
