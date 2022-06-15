FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Discount.API/Discount.API.csproj", "Discount.API/"]
COPY ["Services/Discount/Discount.Domain/Discount.Domain.csproj", "Discount.Domain/"]
COPY ["Services/Discount/Discount.Application/Discount.Application.csproj", "Discount.Application/"]
COPY ["Services/Discount/Discount.Data/Discount.Data.csproj", "Discount.Data/"]
RUN dotnet restore "Services/Discount/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/Discount.API"
RUN dotnet build "Discount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]
