#!/usr/bin/bash

podman run -d -e 'ACCEPT_EULA=Y' -e \
  'MSSQL_SA_PASSWORD=Passw0rd'  \
  --name MySQL1 \
  -p 1460:1433 \
  -v ./Database:/var/mssql/data:Z \
  mcr.microsoft.com/mssql/server:2019-latest
