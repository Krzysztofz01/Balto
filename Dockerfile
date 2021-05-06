FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY Balto.sln ./
COPY ["Balto.Web/Balto.Web.csproj", "Balto.Web/"]
COPY ["Balto.Service/Balto.Service.csproj", "Balto.Service/"]
COPY ["Balto.Domain/Balto.Domain.csproj", "Balto.Domain/"]
COPY ["Balto.Repository/Balto.Repository.csproj", "Balto.Repository/"]
RUN dotnet restore "Balto.Web/Balto.Web.csproj"
COPY . .
WORKDIR "/src/Balto.Web"
RUN dotnet build "Balto.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Balto.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Balto.Web.dll"]