FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build --configuration Release

FROM build AS publish
RUN dotnet publish --configuration Release --no-build -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS release
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 7240:7240
ENTRYPOINT ["dotnet", "CodeCollab - WorkspaceService.dll"]
