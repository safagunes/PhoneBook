#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReportService.API/ReportService.API.csproj", "ReportService.API/"]
COPY ["ReportService.Domain/ReportService.Domain.csproj", "ReportService.Domain/"]
COPY ["ReportService.Infrastructure/ReportService.Infrastructure.csproj", "ReportService.Infrastructure/"]
RUN dotnet restore "ReportService.API/ReportService.API.csproj"
COPY . .
WORKDIR "/src/ReportService.API"
RUN dotnet build "ReportService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.API.dll"]