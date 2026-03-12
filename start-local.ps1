# ModernArch 應用啟動腳本（Windows）

Write-Host "🚀 初始化 ModernArch 應用..." -ForegroundColor Cyan

# 移動到 API 目錄
Set-Location ModernArch.Api

Write-Host "📦 還原 NuGet 套件..." -ForegroundColor Yellow
dotnet restore

Write-Host "🗄️ 刪除舊的數據庫文件（如果存在）..." -ForegroundColor Yellow
Remove-Item -Path "todoapp.db*" -ErrorAction SilentlyContinue

Write-Host "🔄 建立新的 Migration（SQLite）..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreateSQLite --context TodoContext

Write-Host "🔨 應用 Migration 到數據庫..." -ForegroundColor Yellow
dotnet ef database update

Write-Host "✅ 數據庫初始化完成！" -ForegroundColor Green
Write-Host "🌐 啟動應用..." -ForegroundColor Cyan
dotnet run
