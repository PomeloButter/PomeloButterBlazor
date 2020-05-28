#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PomeloButterBlog.Api/PomeloButterBlog.Api.csproj", "PomeloButterBlog.Api/"]
COPY ["PomeloButterBlog.App/PomeloButterBlog.App.csproj", "PomeloButterBlog.App/"]
COPY ["PomeloButterBlog.Common/PomeloButterBlog.Common.csproj", "PomeloButterBlog.Common/"]
RUN dotnet restore "PomeloButterBlog.Api/PomeloButterBlog.Api.csproj"
COPY . .
RUN dotnet build "PomeloButterBlog.Api/PomeloButterBlog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PomeloButterBlog.Api/PomeloButterBlog.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PomeloButterBlog.Api.dll"]
