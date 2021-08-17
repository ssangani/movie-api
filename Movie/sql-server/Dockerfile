# Choose SQL Server image
FROM mcr.microsoft.com/mssql/server:2019-CU12-ubuntu-20.04

# Change to root
USER root

# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Copy files
COPY . /usr/src/app

# Grant permissions
RUN chmod +x /usr/src/app/run-initialization.sh

# Set Env variables
ENV SA_PASSWORD S0up3dUpPassword
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

# Expose port
EXPOSE 1433

# Instantiate DB data and run the SQL server 
CMD /bin/bash ./entrypoint.sh