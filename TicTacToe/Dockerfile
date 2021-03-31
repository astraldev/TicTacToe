FROM mcr.microsoft.com/dotnet/sdk:latest 

RUN mkdir app 

WORKDIR /app

COPY . ./

RUN dotnet publish -c Release -o out

WORKDIR /app

COPY /out .

ENTRYPOINT [ "dotnet", "CSharp.dll" ]
