FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 55504

FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR "/src/OcelotSample.WebFront"
RUN dotnet publish "OcelotSample.WebFront.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ["dotnet", "OcelotSample.WebFront.dll"]

