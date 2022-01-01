FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /src
COPY ./*.csproj .
RUN dotnet restore
# not Production facing so usage of "dotnet run" is fine
ENTRYPOINT ["dotnet", "run"]