FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/API/Discount.API/Discount.API.csproj", "Services/Discount/API/Discount.API/"]
COPY ["Services/Discount/API/Discount.Domain/Discount.Domain.csproj", "Services/Discount/API/Discount.Domain/"]
COPY ["Services/Discount/API/Discount.Application/Discount.Application.csproj", "Services/Discount/API/Discount.Application/"]
COPY ["Services/Discount/API/Discount.Data/Discount.Data.csproj", "Services/Discount/API/Discount.Data/"]
RUN dotnet restore "Services/Discount/API/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/Services/Discount/API/Discount.API"
RUN dotnet build "Discount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]
