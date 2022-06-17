FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Grpc/Discount.Grpc/Discount.Grpc.csproj", "Services/Discount/Grpc/Discount.Grpc/"]
COPY ["Services/Discount/Grpc/Discount.Grpc.Data/Discount.Grpc.Data.csproj", "Services/Discount/Grpc/Discount.Grpc.Data/"]
COPY ["Services/Discount/Grpc/Discount.Grpc.Domain/Discount.Grpc.Domain.csproj", "Services/Discount/Grpc/Discount.Grpc.Domain/"]
COPY ["Services/Discount/Grpc/Discount.Grpc.Application/Discount.Grpc.Application.csproj", "Services/Discount/Grpc/Discount.Grpc.Application/"]
RUN dotnet restore "Services/Discount/Grpc/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Grpc/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]
