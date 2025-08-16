# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY AIChatbotWebApi/AIChatbotWebApi.csproj ./
RUN dotnet restore

# Copy all source files and publish
COPY AIChatbotWebApi/ ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "AIChatbotWebApi.dll"]
