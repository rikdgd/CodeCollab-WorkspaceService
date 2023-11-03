FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS release
EXPOSE 7240:7240
WORKDIR /app
COPY --from=build /app/codecollab-backend/bin/Release .
RUN dotnet run
