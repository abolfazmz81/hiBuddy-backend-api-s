FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Media.Presentation/Media.Presentation.csproj", "Media.Presentation/"]
RUN dotnet restore "Media.Presentation/Media.Presentation.csproj"
COPY . .
WORKDIR "/src/Media.Presentation"
RUN dotnet build "Media.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Media.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Media.Presentation.dll"]
