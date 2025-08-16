# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY AIChatbotWebApi/AIChatbotWebApi.csproj ./
RUN dotnet restore

# Copy everything from the project folder
COPY AIChatbotWebApi/ ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "AIChatbotWebApi.dll"]
