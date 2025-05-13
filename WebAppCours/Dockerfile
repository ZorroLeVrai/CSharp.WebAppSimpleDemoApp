FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine

WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Expose the port the app runs on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "out/WebAppCours.dll"]
