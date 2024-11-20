FROM mcr.microsoft.com/mssql/server:2022-latest

ENV MSSQL_SA_PASSWORD=YourStrong!Passw0rd
ENV ACCEPT_EULA=Y

EXPOSE 1433

COPY SampleDB.sql /usr/src/app/SampleDB.sql
COPY SQLServer.sh /usr/src/app/entrypoint.sh

USER root
RUN chmod +x /usr/src/app/entrypoint.sh

ENTRYPOINT ["/bin/bash", "/usr/src/app/entrypoint.sh"]