# Dev Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Copy toàn bộ source vào container
COPY . .

# Restore và chạy trực tiếp
RUN dotnet restore

# Chạy ứng dụng ở chế độ Development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_HOST_PATH=/usr/share/dotnet/dotnet

# Expose port 80 trong container
EXPOSE 80

ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://0.0.0.0:80"]
