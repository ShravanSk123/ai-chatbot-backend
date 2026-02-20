# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AIChatbotWebApi/AIChatbotWebApi.csproj ./
RUN dotnet restore

COPY AIChatbotWebApi/ ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

# IMPORTANT: Bind to dynamic port
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "AIChatbotWebApi.dll"]
