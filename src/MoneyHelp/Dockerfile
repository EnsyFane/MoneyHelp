FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS publish
WORKDIR /src

COPY . .
RUN dotnet restore "./MoneyHelp.Api/MoneyHelp.Api.csproj" --runtime alpine-x64

RUN dotnet publish "./MoneyHelp.Api/MoneyHelp.Api.csproj" -c Release -o /app/publish \
  --no-restore \
  --runtime alpine-x64 \
  --self-contained true 

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS final
ENV COMPlus_EnableDiagnostics=0
WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 80

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser
RUN mkdir /app/Files
RUN chown appuser /app/Files

ENTRYPOINT ["./TheSeater.Api"]
