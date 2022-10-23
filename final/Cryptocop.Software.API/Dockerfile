FROM mcr.microsoft.com/dotnet/sdk:6.0
LABEL "maintainer"="Gísli Guðmundsson"

ARG TAG=cryptocop
ARG NAME=cryptocop
ENV ASPNETCORE_URLS=http://+:5000

COPY . /app
WORKDIR /app/Cryptocop.Software.API

RUN dotnet restore

ENTRYPOINT ["dotnet", "run"]