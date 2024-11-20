FROM mcr.microsoft.com/mssql/server:2022-latest

ENV SA_PASSWORD=YourStrong!Passw0rd
ENV ACCEPT_EULA=Y

COPY SampleDB.sql /usr/src/app/SampleDB.sql
COPY SQLServer.sh /usr/src/app/entrypoint.sh

RUN chmod +x /usr/src/app/entrypoint.sh

ENTRYPOINT ["/bin/bash", "/usr/src/app/entrypoint.sh"]