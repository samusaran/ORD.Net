# https://docs.docker.com/engine/examples/dotnetcore/#prerequisites
FROM microsoft/aspnetcore-build:latest AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore --verbosity detailed

# Copy everything else and build
RUN dotnet publish ORD.NET.Service -c Release -o /app/out

# Build runtime image
FROM microsoft/aspnetcore:2.0.5
EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ORD.NET.Service.dll"]
