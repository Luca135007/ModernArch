#!/bin/bash

echo "⚡ 快速啟動 ModernArch（Codespaces）..."

cd ModernArch.Api

# 設置環境變數
export ASPNETCORE_ENVIRONMENT=Development
export PORT=8080
export CODESPACES=true

# 如果數據庫不存在，創建它
if [ ! -f "todoapp.db" ]; then
    echo "🗄️ 初始化數據庫..."
    dotnet ef database update
fi

echo "🌐 啟動應用於端口 8080..."
echo "📡 您可以在 Codespaces 的 PORTS 標籤頁中找到轉發的 URL"
echo ""

# 啟動應用
dotnet run --urls "http://0.0.0.0:8080"
