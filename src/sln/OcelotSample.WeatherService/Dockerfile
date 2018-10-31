FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50379

FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR "/src/OcelotSample.WeatherService"
RUN dotnet publish "OcelotSample.WeatherService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "OcelotSample.WeatherService.dll"]
