FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY *.sln ./
COPY ApiAccessControl ./ApiAccessControl/
COPY Security.Business/*.csproj ./Security.Business/
COPY Security.DataAccess/*.csproj ./Security.DataAccess/
COPY Security.DataAccess/bin/Release/netcoreapp2.2 ./Security.DataAccess/
COPY Security.Api/*.csproj ./Security.Api/
RUN dotnet restore 
COPY . .
WORKDIR /app/Security.Business/
RUN dotnet build -c Release -o /app
WORKDIR /app/Security.DataAccess/
RUN dotnet publish -c Release -o /app 
WORKDIR /app/Security.Api/
RUN dotnet publish --configuration Debug --output /output


# Build image runtime!
FROM microsoft/dotnet:aspnetcore-runtime
MAINTAINER eduardo.prudencio@inovaideia.net.br
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "Security.Api.dll"]