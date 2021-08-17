# wait for container to spin up
./wait-for-it.sh movie-db:1433
# run set up script
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S0up3dUpPassword -d master -i setup.sql