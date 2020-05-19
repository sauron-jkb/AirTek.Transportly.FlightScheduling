#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AirTek.Transportly.FlightScheduling.Presentation.Console/AirTek.Transportly.FlightScheduling.Presentation.Console.csproj", "AirTek.Transportly.FlightScheduling.Presentation.Console/"]
RUN dotnet restore "AirTek.Transportly.FlightScheduling.Presentation.Console/AirTek.Transportly.FlightScheduling.Presentation.Console.csproj"
COPY . .
WORKDIR "/src/AirTek.Transportly.FlightScheduling.Presentation.Console"
RUN dotnet build "AirTek.Transportly.FlightScheduling.Presentation.Console.csproj" -c Release -o /app/build

COPY AirTek.Transportly.FlightScheduling.Infrastructure.Model/bin/Release/orders.json /app/build/

FROM build AS publish
RUN dotnet publish "AirTek.Transportly.FlightScheduling.Presentation.Console.csproj" -c Release -o /app/publish

WORKDIR /app
COPY --from=build /app/build/orders.json /app/publish/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirTek.Transportly.FlightScheduling.Presentation.Console.dll"]