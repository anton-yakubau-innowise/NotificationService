FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 8080

COPY ["NotificationService.sln", "NotificationService.sln"]
COPY ["NotificationService.API/NotificationService.API.csproj", "NotificationService.API/"]
COPY ["NotificationService.Application/NotificationService.Application.csproj", "NotificationService.Application/"]
COPY ["NotificationService.Domain/NotificationService.Domain.csproj", "NotificationService.Domain/"]
COPY ["NotificationService.Infrastructure/NotificationService.Infrastructure.csproj", "NotificationService.Infrastructure/"]

RUN dotnet restore "NotificationService.sln"

COPY . .

WORKDIR "/src/NotificationService.API"
RUN dotnet publish "NotificationService.API.csproj" -c Release -o /app/publish





FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /src/NotificationService.API/app/publish .


ENTRYPOINT ["dotnet", "NotificationService.API.dll"]