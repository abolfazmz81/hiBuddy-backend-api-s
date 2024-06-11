FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["User.Presentation/User.Presentation.csproj", "User.Presentation/"]
RUN dotnet restore "User.Presentation/User.Presentation.csproj"
COPY . .
WORKDIR "/src/User.Presentation"
RUN dotnet build "User.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Presentation.dll"]
