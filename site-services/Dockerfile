#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["site-services.csproj", "site-services/"]
RUN dotnet restore "site-services/site-services.csproj"
WORKDIR "/src/site-services"
COPY . .
RUN dotnet build "site-services.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "site-services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=https://+:8080
COPY --from=publish /app/publish .
RUN echo $ASPNETCORE_URLS
ENTRYPOINT ["dotnet", "site-services.dll"]