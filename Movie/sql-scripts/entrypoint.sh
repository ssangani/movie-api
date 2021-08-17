#!/bin/bash

# Start the initialization script and start the server
# Running the server is main command, which is why it's last. Otherwise
# the container will exit. The initialization script is configured
# to wait till server is up to run commands
/usr/scripts/run-initialization.sh & /opt/mssql/bin/sqlservr