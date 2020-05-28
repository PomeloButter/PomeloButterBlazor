#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY PomeloButterBlog.Api.csproj.
RUN dotnet restore PomeloButterBlog.Api.csproj
COPY . .
RUN dotnet build "PomeloButterBlog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PomeloButterBlog.Api.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/PomeloButterBlog/dist .
COPY nginx.conf /etc/nginx/nginx.conf
