FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PM.WebAPI/PM.WebAPI.csproj", "PM.WebAPI/"]
COPY ["PM.Application/PM.Application.csproj", "PM.Application/"]
COPY ["PM.Domain/PM.Domain.csproj", "PM.Domain/"]
COPY ["PM.Data/PM.Data.csproj", "PM.Data/"]
COPY ["PM.Core/PM.Common.csproj", "PM.Core/"]
COPY ["PM.Localizations/PM.Localizations.csproj", "PM.Localizations/"]
COPY ["PM.Components/PM.Components.csproj", "PM.Components/"]
COPY ["PM.Infrastructure/PM.Infrastructure.csproj", "PM.Infrastructure/"]
COPY ["PM.WebApp/PM.WebApp.csproj", "PM.WebApp/"]
RUN dotnet restore "PM.WebAPI/PM.WebAPI.csproj"
COPY . .
WORKDIR "/src/PM.WebAPI"
RUN dotnet build "PM.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PM.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /db

ARG ARG_ASPNETCORE_ENVIRONMENT="Development"
ARG ARG_ASPNETCORE_URLS
ARG ARG_PM_ApplicationUsers__Admin__Email="i4koo@abv.bg"
ARG ARG_PM_ApplicationUsers__Admin__Password="!Admin1"
ARG ARG_PM_ApplicationUsers__Admin__UserName="admin"
ARG ARG_PM_ConnectionStrings__SqliteConnection="Data Source=:memory:;"
ARG ARG_PM_GoogleReCaptcha__SecretKey="6LeMp9MZAAAAAL5SBdvSkmrwIdRp9ioZs476ICEM"
ARG ARG_PM_GoogleReCaptcha__SiteKey="6LeMp9MZAAAAAKt5DdbOxSTDvfhrP-YzqFfjMqlX"
ARG ARG_PM_SendGrid__APIKey
ARG ARG_PM_SendGrid__FromEmail
ARG ARG_PM_SendGrid__FromName
ARG ARG_PM_SendGrid__ToEmail
ARG ARG_PM_SendGrid__ToName
ARG ARG_PORT="80"

ENV ASPNETCORE_ENVIRONMENT=${ARG_ASPNETCORE_ENVIRONMENT}
ENV ASPNETCORE_URLS=${ARG_ASPNETCORE_URLS}
ENV PM_ApplicationUsers__Admin__Email=${ARG_PM_ApplicationUsers__Admin__Email}
ENV PM_ApplicationUsers__Admin__Password=${ARG_PM_ApplicationUsers__Admin__Password}
ENV PM_ApplicationUsers__Admin__UserName=${ARG_PM_ApplicationUsers__Admin__UserName}
ENV PM_ConnectionStrings__SqliteConnection=${ARG_PM_ConnectionStrings__SqliteConnection}
ENV PM_GoogleReCaptcha__SecretKey=${ARG_PM_GoogleReCaptcha__SecretKey}
ENV PM_GoogleReCaptcha__SiteKey=${ARG_PM_GoogleReCaptcha__SiteKey}
ENV PM_SendGrid__APIKey=${ARG_PM_SendGrid__APIKey}
ENV PM_SendGrid__FromEmail=${ARG_PM_SendGrid__FromEmail}
ENV PM_SendGrid__FromName=${ARG_PM_SendGrid__FromName}
ENV PM_SendGrid__ToEmail=${ARG_PM_SendGrid__ToEmail}
ENV PM_SendGrid__ToName=${ARG_PM_SendGrid__ToName}
ENV PORT=${ARG_PORT}

EXPOSE ${PORT}

ENTRYPOINT ["dotnet", "PM.WebAPI.dll"]