#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
RUN npm config set registry https://registry.npm.taobao.org
WORKDIR /src
COPY ["src/Summer.Web/Summer.Web.csproj", "src/Summer.Web/"]
COPY ["src/Summer.App/Summer.App.csproj", "src/Summer.App/"]
COPY ["src/Summer.App.Contracts/Summer.App.Contracts.csproj", "src/Summer.App.Contracts/"]
RUN dotnet restore "src/Summer.Web/Summer.Web.csproj"
COPY . .
WORKDIR "/src/src/Summer.Web"
RUN dotnet build "Summer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Summer.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Summer.Web.dll"]