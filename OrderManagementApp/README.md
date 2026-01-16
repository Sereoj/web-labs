# OrderManagementApp

Приложение управления заказами на ASP.NET Core с использованием C#

## Описание

Веб-приложение, позволяющее:
- Просматривать список товаров
- Добавлять товары в заказ
- Изменять количество товаров в заказе
- Удалять товары из заказа
- Просматривать общую сумму заказа

## Требования

- .NET 8 SDK или выше
- IDE (Visual Studio, VS Code или другая)

## Установка и запуск

1. Установите .NET 8 SDK с официального сайта: https://dotnet.microsoft.com/download
2. Склонируйте или скачайте этот проект
3. Откройте терминал и перейдите в директорию проекта
4. Выполните команду для восстановления зависимостей:

```
dotnet restore
```

5. Запустите приложение:

```
dotnet run
```

6. Откройте браузер и перейдите по адресу: https://localhost:7000

## Архитектура

Проект использует архитектурный шаблон MVC (Model-View-Controller):

- **Models**: Содержит классы данных (Product, OrderItem, Order)
- **Controllers**: Содержит логику обработки запросов (HomeController)
- **Views**: Содержит представления (Index.cshtml)
- **ViewModels**: Содержит модели представлений (OrderViewModel)

## Особенности реализации

- Хранение данных заказа в сессии
- Динамическое обновление заказа с помощью JavaScript
- Валидация данных на стороне сервера
- Адаптивный дизайн с использованием Bootstrap

## Структура проекта

```
OrderManagementApp/
│
├── Controllers/
│   └── HomeController.cs
├── Models/
│   ├── Product.cs
│   ├── OrderItem.cs
│   └── Order.cs
├── ViewModels/
│   └── OrderViewModel.cs
├── Views/
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Shared/
│   │   └── _Layout.cshtml
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   └── js/
│       └── site.js
├── Properties/
│   └── launchSettings.json
├── OrderManagementApp.csproj
└── Program.cs
```