#DOCKER FILE PARA DEPLOY DA APLICAÇÃO DE API

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build

WORKDIR /src
COPY PlayerShares.sln ./
COPY ["PlayerShares.API/*.csproj", "./PlayerShares.API/"]
COPY ["PlayerShares.Application/*.csproj", "./PlayerShares.Application/"]
COPY ["PlayerShares.Domain/*.csproj", "./PlayerShares.Domain/"]
COPY ["PlayerShares.Infra/*.csproj", "./PlayerShares.Infra/"]
COPY ["PlayerShares.UI/*.csproj", "./PlayerShares.UI/"]
COPY ["PlayerShares.Tests/*.csproj", "./PlayerShares.Tests/"]

RUN dotnet restore 


COPY PlayerShares.API/. ./PlayerShares.API/
COPY PlayerShares.Application/. ./PlayerShares.Application/
COPY PlayerShares.Infra/. ./PlayerShares.Infra/ 
COPY PlayerShares.Domain/. ./PlayerShares.Domain/ 

#RUN dotnet test
#COPY PlayerShares.Tests/. ./PlayerShares.Tests/ 


WORKDIR /src/PlayerShares.API 
#FROM build AS publish
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PlayerShares.API.dll"]
