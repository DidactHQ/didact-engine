WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

WORKDIR /app
ENTRYPOINT ["dotnet", "DidactEngine.dll"]
