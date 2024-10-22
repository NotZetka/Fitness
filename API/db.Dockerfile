FROM mcr.microsoft.com/mssql/server:latest AS sqlserver
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=myPassword1!

ENV MSSQL_DBNAME=FitnessApp