FROM mcr.microsoft.com/dotnet/core/sdk AS build-env
WORKDIR /app

# Copy everything and build
COPY . ./

RUN dotnet restore "./Atento.FileManager.Web.Api.csproj"
RUN dotnet publish "Atento.FileManager.Web.Api.csproj" -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Atento.FileManager.Web.Api.dll"]