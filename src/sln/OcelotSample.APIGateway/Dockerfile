FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 59495

FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR "/src/OcelotSample.APIGateway"
RUN dotnet publish "OcelotSample.APIGetway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "OcelotSample.APIGateway.dll"]

