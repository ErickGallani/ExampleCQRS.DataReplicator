FROM microsoft/dotnet:2.2-sdk-alpine AS build-env
WORKDIR /app

COPY . ./

RUN dotnet restore src/1.Application/ExampleCQRS.DataReplicator.App/ExampleCQRS.DataReplicator.App.csproj

RUN dotnet publish src/1.Application/ExampleCQRS.DataReplicator.App/ExampleCQRS.DataReplicator.App.csproj -c Release -o out


FROM microsoft/dotnet:2.2-runtime-alpine

WORKDIR /app

COPY --from=build-env /app/src/1.Application/ExampleCQRS.DataReplicator.App/out .

ENTRYPOINT ["dotnet", "ExampleCQRS.DataReplicator.App.dll"]