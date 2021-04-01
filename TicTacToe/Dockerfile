FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env

RUN mkdir app 

WORKDIR /app

COPY . ./

RUN dotnet publish -c Release -o out

WORKDIR /app

FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "TicTacToe.dll" ]
