# 🚀 在 GitHub Codespaces 中运行 ModernArch

## 📋 问题解决方案

如果您在 Codespaces 中遇到 "无法连接 API" 错误，这是因为应用需要特殊配置才能在 Codespaces 环境中运行。

## ✅ 已完成的修复

1. ✅ **端口配置**：应用现在监听 `0.0.0.0:8080`（Codespaces 标准端口）
2. ✅ **环境检测**：自动检测 Codespaces 环境
3. ✅ **数据库**：使用 SQLite（跨平台支持）
4. ✅ **CORS 配置**：已启用跨域请求

## 🎯 快速启动步骤

### 在 GitHub Codespaces 中：

```bash
# 方式 1: 使用快速启动脚本（推荐）
chmod +x quick-start.sh
./quick-start.sh

# 方式 2: 使用完整初始化脚本
chmod +x start-codespaces.sh
./start-codespaces.sh

# 方式 3: 手动启动
cd ModernArch.Api
export PORT=8080
export ASPNETCORE_ENVIRONMENT=Development
dotnet ef database update
dotnet run --urls "http://0.0.0.0:8080"
```

### 访问应用：

1. **等待应用启动**（大约 5-10 秒）
2. **查看 PORTS 标签页**（在 VS Code 底部）
3. **找到端口 8080** 并点击地球图标 🌐
4. **或者点击弹出的通知** "Open in Browser"

## 🔍 调试步骤

如果仍然无法连接：

### 1. 检查应用是否运行
```bash
cd ModernArch.Api
dotnet run --urls "http://0.0.0.0:8080"
```

应该看到：
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://0.0.0.0:8080
```

### 2. 检查端口转发

在 VS Code 中：
1. 打开 **PORTS** 标签页（底部面板）
2. 确保端口 **8080** 在列表中
3. 确保 **Visibility** 设置为 **Public**
4. 点击 **Local Address** 列的链接

### 3. 测试 API 连接

在 Codespaces 终端中：
```bash
# 测试本地 API
curl http://localhost:8080/api/Todo

# 应该返回 [] 或待办事项列表
```

### 4. 检查浏览器控制台

1. 按 **F12** 打开开发者工具
2. 查看 **Console** 标签页
3. 查看 **Network** 标签页中的失败请求
4. 检查请求的 URL 是否正确

## 📊 预期输出

启动成功后，您应该看到：

```
✅ 数据库初始化完成！
🌐 启动应用於端口 8080...
📡 您可以在 Codespaces 的 PORTS 标签页中找到转发的 URL

info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://0.0.0.0:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

## 🔧 环境变量

应用会自动检测以下环境变量：

- `CODESPACES=true` - 检测 Codespaces 环境
- `PORT=8080` - 指定监听端口
- `ASPNETCORE_ENVIRONMENT=Development` - 开发模式

## 📁 数据库位置

SQLite 数据库文件位于：
```
ModernArch.Api/todoapp.db
```

如果需要重置数据库：
```bash
cd ModernArch.Api
rm -f todoapp.db*
dotnet ef database update
```

## 🌐 API 端点

基础 URL（在 Codespaces 中）：
```
https://obscure-garbanzo-xxx.app.github.dev/api/Todo
```

可用端点：
- `GET /api/Todo` - 获取所有待办事项
- `GET /api/Todo/{id}` - 获取单个待办事项
- `POST /api/Todo` - 创建待办事项
- `PUT /api/Todo/{id}` - 更新待办事项
- `DELETE /api/Todo/{id}` - 删除待办事项

## ❓ 常见问题

### Q: 为什么要监听 0.0.0.0 而不是 localhost？
**A**: Codespaces 使用反向代理。监听 `0.0.0.0` 允许外部流量通过端口转发访问应用。

### Q: 可以使用不同的端口吗？
**A**: 可以，但需要更新：
- `.devcontainer/devcontainer.json` 中的 `forwardPorts`
- `quick-start.sh` 中的 `PORT` 变量
- Codespaces PORTS 标签页中手动添加新端口

### Q: 数据会保留吗？
**A**: 在 Codespaces 运行期间会保留。停止 Codespaces 后，数据存储在持久化卷中。删除 Codespaces 会丢失数据。

## 💡 提示

- 使用 `quick-start.sh` 进行快速启动
- 数据库会自动创建和迁移
- 如果端口被占用，可以在启动时指定不同端口：
  ```bash
  PORT=3000 dotnet run --urls "http://0.0.0.0:3000"
  ```

## 🎉 成功指标

应用运行成功的标志：
1. ✅ 终端显示 "Now listening on: http://0.0.0.0:8080"
2. ✅ PORTS 标签页显示端口 8080 转发
3. ✅ 浏览器打开应用，可以添加/删除待办事项
4. ✅ 浏览器控制台没有错误

---

**需要帮助？** 检查浏览器控制台 (F12) 和终端输出以获取详细错误信息。
