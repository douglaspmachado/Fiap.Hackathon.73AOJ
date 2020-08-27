#DOCKER FILE PARA DEPLOY DA APLICAÇÃO DE API

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build

WORKDIR /src
COPY Hackathon.Fiap.sln ./
COPY ["App.API/*.csproj", "./App.API/"]
COPY ["App.Application/*.csproj", "./App.Application/"]
COPY ["App.Domain/*.csproj", "./App.Domain/"]
COPY ["App.Infra/*.csproj", "./App.Infra/"]
COPY ["App.UI/*.csproj", "./App.UI/"]
COPY ["App.Tests/*.csproj", "./App.Tests/"]

RUN dotnet restore 


COPY App.API/. ./App.API/
COPY App.Application/. ./App.Application/
COPY App.Infra/. ./App.Infra/ 
COPY App.Domain/. ./App.Domain/ 

#RUN dotnet test
#COPY PlayerShares.Tests/. ./PlayerShares.Tests/ 


WORKDIR /src/App.API 
#FROM build AS publish
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "App.API.dll"]
