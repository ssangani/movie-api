#!/bin/bash

# wait for container to spin up
./wait-for-it.sh "$DB_HOST:1433" || exit 1
echo "sql server container ready"

# run init script
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S0up3dUpPassword -d master -i setup.sql
echo "database initialization complete"