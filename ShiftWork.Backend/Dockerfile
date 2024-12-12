#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# SET the working directory
WORKDIR /app
# copy the project files
COPY . .
# Restore and build the project
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Final stage: run the application using ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 80
# Set the application to listen on IPv4 only
ENV ASPNETCORE_URLS=http://0.0.0.0:80
# Run the application
ENTRYPOINT ["dotnet", "ShiftWork.Backend.dll"]