# # Build stage
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /app

# COPY *.csproj ./
# RUN dotnet restore

# COPY . .
# RUN dotnet publish -c Release -o out

# # Runtime stage
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app/out .
# EXPOSE 7058

# ENTRYPOINT ["dotnet", "dotnnet-api.dll"]
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj
COPY dotnnet-api/*.csproj dotnnet-api/
WORKDIR /src/dotnnet-api
RUN dotnet restore

# copy everything else
COPY dotnnet-api/. .
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 7058

ENTRYPOINT ["dotnet", "dotnnet-api.dll"]
