#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Ordering/API/API.csproj", "Services/Ordering/API/"]
COPY ["Services/Ordering/Application/Application.csproj", "Services/Ordering/Application/"]
COPY ["Services/Ordering/Domain/Domain.csproj", "Services/Ordering/Domain/"]
COPY ["Services/Ordering/Infrastructure/Infrastructure.csproj", "Services/Ordering/Infrastructure/"]
RUN dotnet restore "Services/Ordering/API/API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]