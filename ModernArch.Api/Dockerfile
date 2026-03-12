# 1. 建置階段 (Build Stage)：使用包含 SDK 的映像檔來編譯程式碼
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 複製專案檔 (.csproj) 並還原 NuGet 套件
COPY ["*.csproj", "./"]
RUN dotnet restore

# 複製所有原始碼，並發布成編譯後的檔案 (Release 模式)
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ---------------------------------------------------

# 2. 執行階段 (Run Stage)：使用只包含 Runtime 的輕量級映像檔
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 開放 8080 port (ASP.NET Core 8 預設的 port)
EXPOSE 8080

# 把建置階段做好的檔案，複製到這個最終的映像檔裡
COPY --from=build /app/publish .

# 告訴 Docker 啟動時要執行哪個檔案
# ⚠️ 注意：請確保這裡的 dll 名稱與你的專案名稱完全一致！
ENTRYPOINT ["dotnet", "ModernArch.Api.dll"]
