# Calculator_DotNet
![計算機圖示](https://cdn-images-1.medium.com/v2/resize:fit:1200/1*nyZa_hBoJPVHNam1rg-Q_A.png)

[本程式的Medium教學文章](https://yanwei-liu.medium.com/how-to-build-calculator-app-in-csharp-dotnet-with-gtksharp-903552d67634)

這是一個使用 .NET 和 GtkSharp 開發的簡單計算機應用程式。該應用程式提供基本的算術運算功能，包括加法、減法、乘法和除法，並支持鍵盤輸入。

## 目錄結構
```bash
├── .gitignore          # Git 忽略文件
├── Calculator.csproj   # .NET 專案文件
├── LICENSE             # License文件
├── MainWindow.cs       # 主視窗程式碼
├── Program.cs          # 應用程式
└── README.md           # README文件
```

## 功能

- 支持基本的算術運算：加法、減法、乘法和除法。
- 支持鍵盤輸入，方便用戶操作。
- 簡單直觀的用戶界面。

## 環境要求

- .NET 9.0 或更高版本
- GtkSharp 3.24.24.38

## 安裝與運行

1. Clone Repo：
   
```bash
git clone https://github.com/yourusername/Calculator_DotNet.git
cd Calculator_DotNet
```

2. 使用 .NET CLI Build Project：
```bash
dotnet build
dotnet run
```
## 使用說明
- 點擊數字按鈕或使用鍵盤數字鍵輸入數字。
- 點擊運算符按鈕或使用鍵盤運算符鍵進行運算。
- 點擊 "=" 按鈕或按 Enter 鍵計算結果。
- 點擊 "清除" 按鈕或按 Escape 鍵清除計算器。
## 授權
本項目使用 MIT 許可證。詳情請參見 LICENSE 文件。

## 貢獻
歡迎任何形式的貢獻！請開Issues或Pull requests。

## 聯繫
如有任何問題或建議，請聯繫[Yanwei Liu](https://github.com/e96031413)。
