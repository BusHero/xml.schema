FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["XmlSchemaApi/XmlSchemaApi.csproj", "XmlSchemaApi/"]
RUN dotnet restore "XmlSchemaApi/XmlSchemaApi.csproj"
COPY . .
WORKDIR "/src/XmlSchemaApi"
RUN dotnet build "XmlSchemaApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XmlSchemaApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XmlSchemaApi.dll"]
