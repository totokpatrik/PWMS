FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0-jammy-chiseled
EXPOSE 8080
EXPOSE 8081

USER app

WORKDIR /home/app

COPY ./src/My.Nkz.NewApp.Presentation.Starter/publish/app .

ENTRYPOINT ["dotnet", "My.Nkz.NewApp.Presentation.Starter.dll"]
