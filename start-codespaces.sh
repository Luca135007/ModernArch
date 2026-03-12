#!/bin/bash

echo "🚀 初始化 ModernArch 應用（Codespaces）..."

# 移動到 API 目錄
cd ModernArch.Api

echo "📦 還原 NuGet 套件..."
dotnet restore

echo "🗄️ 刪除舊的數據庫文件（如果存在）..."
rm -f todoapp.db todoapp.db-shm todoapp.db-wal

echo "🔄 檢查 Migrations..."
if [ ! -d "Migrations" ]; then
    echo "📋 建立新的 Migration..."
    dotnet ef migrations add InitialCreateSQLite --context TodoContext
fi

echo "🔨 應用 Migration 到數據庫..."
dotnet ef database update

echo "✅ 數據庫初始化完成！"
echo "🌐 啟動應用於端口 8080..."

# 設置環境變數
export ASPNETCORE_ENVIRONMENT=Development
export PORT=8080

# 啟動應用，監聽所有網路介面
dotnet run --urls "http://0.0.0.0:8080"
