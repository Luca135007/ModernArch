#!/bin/bash

echo "🚀 初始化 ModernArch 應用..."

# 移動到 API 目錄
cd ModernArch.Api

echo "📦 還原 NuGet 套件..."
dotnet restore

echo "🗄️ 刪除舊的數據庫文件（如果存在）..."
rm -f todoapp.db todoapp.db-shm todoapp.db-wal

echo "🔄 刪除舊的 Migrations（如果需要重新生成）..."
# 取消註解下一行如果需要重新生成 migrations
# rm -rf Migrations

echo "📋 建立新的 Migration（如果尚未存在）..."
dotnet ef migrations add InitialCreateSQLite --context TodoContext

echo "🔨 應用 Migration 到數據庫..."
dotnet ef database update

echo "✅ 數據庫初始化完成！"
echo "🌐 啟動應用..."
dotnet run
