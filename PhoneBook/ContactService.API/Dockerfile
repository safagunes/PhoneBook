#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactService.API/ContactService.API.csproj", "ContactService.API/"]
COPY ["ContactService.Domain/ContactService.Domain.csproj", "ContactService.Domain/"]
COPY ["ContactService.Infrastructure/ContactService.Infrastructure.csproj", "ContactService.Infrastructure/"]
RUN dotnet restore "ContactService.API/ContactService.API.csproj"
COPY . .
WORKDIR "/src/ContactService.API"
RUN dotnet build "ContactService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactService.API.dll"]