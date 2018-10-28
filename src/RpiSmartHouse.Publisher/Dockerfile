FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-arm32v7 AS runtime
FROM microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim AS runtime

WORKDIR /app

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "RpiSmartHouse.Publisher.dll"]
