FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as builder
WORKDIR /src

WORKDIR /src/
COPY [".", "."]
RUN dotnet publish -c Release "./ApiAppNetCore/ApiAppNetCore.csproj" -o /src/published

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
## Do NOT Remove This
COPY --from=builder /src/published/ /opt/api
WORKDIR /opt/api

ENV PORT=80
ENTRYPOINT [ "sh", "-c", "dotnet ApiAppNetCore.dll --urls http://0.0.0.0:${PORT}" ]
EXPOSE 80
