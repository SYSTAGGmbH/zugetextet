FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV TZ Europe/Berlin
RUN useradd -u 5666 nonroot && \
    mkdir /app/App_Data && \
    chmod -R 777 /app/App_Data

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV GENERATE_SOURCEMAP false

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
    nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["zugetextet.formulare.csproj", "."]
RUN dotnet restore "zugetextet.formulare.csproj"
COPY . .
RUN dotnet build "zugetextet.formulare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zugetextet.formulare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish --chown=nonroot:nonroot /app/publish .
USER nonroot:nonroot
ENV ASPNETCORE_URLS=http://+:5000
#  Ban  AOT
ENV DOTNET_ReadyToRun 0
#  Turn on layering  PGO
ENV DOTNET_TieredPGO 1
#  Enable... For loops  Quick Jit
ENV DOTNET_TC_QuickJitForLoops 1
# HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost:5000/health || exit 1
ENTRYPOINT ["dotnet", "zugetextet.formulare.dll"]
