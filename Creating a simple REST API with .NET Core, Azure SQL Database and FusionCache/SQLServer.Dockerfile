FROM mcr.microsoft.com/mssql/server:2022-latest

ENV MSSQL_SA_PASSWORD=YourStrong!Passw0rd
ENV ACCEPT_EULA=Y

EXPOSE 1433

# Install mssql-tools
USER root
RUN apt-get update && \
apt-get install -y curl apt-transport-https && \
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
apt-get update && \
ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools && \
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
/bin/bash -c "source ~/.bashrc"

COPY SampleDB.sql /usr/src/app/SampleDB.sql
COPY SQLServer.sh /usr/src/app/entrypoint.sh

USER root
RUN chmod +x /usr/src/app/entrypoint.sh

ENTRYPOINT ["/bin/bash", "/usr/src/app/entrypoint.sh"]