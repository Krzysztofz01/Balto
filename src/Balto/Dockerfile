FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Balto.API/Balto.API.csproj", "Balto.API/"]
COPY ["Balto.Infrastructure.MySql/Balto.Infrastructure.MySql.csproj", "Balto.Infrastructure.MySql/"]
COPY ["Balto.Infrastructure.Core/Balto.Infrastructure.Core.csproj", "Balto.Infrastructure.Core/"]
COPY ["Balto.Domain/Balto.Domain.csproj", "Balto.Domain/"]
COPY ["Balto.Domain.Core/Balto.Domain.Core.csproj", "Balto.Domain.Core/"]
COPY ["Balto.Application/Balto.Application.csproj", "Balto.Application/"]
RUN dotnet restore "Balto.API/Balto.API.csproj"
COPY . .
WORKDIR "/src/Balto.API"
RUN dotnet build "Balto.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Balto.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Balto.API.dll"]