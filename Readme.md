# 🪟 Система управления UI окнами в Unity

Этот репозиторий содержит первую верисию архитектуры управления окнами в Unity, реализующую модульную, расширяемую и масштабируемую систему показа окон с возможностью стековой навигации, фабриками презентеров и моделей.

## 🧠 Архитектура

Проект построен на следующих принципах:

- **MVP-подход (Model-View-Presenter)**: разделение логики, представления и модели.
- **Интерфейсная абстракция**: использование интерфейсов для модульности и подмены реализаций.
- **DI-подход**: зависимости передаются через конструкторы.
- **Стек окон**: активное окно скрывается при открытии нового, и восстанавливается при его закрытии.

## 📂 Основные компоненты

| Компонент                    | Назначение                                                              |
|-----------------------------|-------------------------------------------------------------------------|
| `IWindow`                   | Интерфейс представления окна                                            |
| `IPresenter`                | Интерфейс логики окна (презентера)                                      |
| `IWindowService`            | Сервис отображения окон и управления стеком                             |
| `WindowsContainer`          | Контейнер, управляющий UI Canvas и стеком окон                          |
| `IWindowFactory`            | Создание экземпляров окон из префабов                                   |
| `IPresentersFactory`        | Создание презентеров с моделью, окном и медиатором                      |
| `IModelsFactory`            | Генерация моделей окна                                                  |
| `IPathWindowProvider`       | Хранение путей к префабам окон                                          |
| `IWindowCloseMediator`      | Посредник для обработки событий закрытия окна                           |

## 🛠️ Установка и использование

1. **Добавьте `WindowsContainer`** в сцену Unity и настройте Canvas.
2. **Зарегистрируйте фабрики и зависимости** (в примере используется `EntryPoint`).
3. **Создайте окно, модель и презентер**, реализуя их через базовые классы.
4. **Открывайте окно** через `IWindowService.Show<TWindow, TModel, TPresenter>()`.

## 🚀 Пример открытия окна

```csharp
await _windowService.Show<PreviewPuzzleWindow, PreviewPuzzleModel, PreviewPuzzlePresenter>();
```

## 🧱 Структура базовых классов

### `BasePresenter<TModel, TWindow>`
- Хранит модель и представление окна
- Подписывается на события окна
- Отвечает за логику взаимодействия

### `BaseWindowModel`
- Данные, необходимые для отображения окна

### `IWindow`
- Unity-компонент с `RectTransform` и событиями (например, кнопка закрытия)

## 🧪 Пример реализации

```csharp
public class ConfirmationWindowPresenter : BasePresenter<ConfirmationWindowModel, ConfirmationWindowView>
{
    public ConfirmationWindowPresenter(
        ConfirmationWindowModel model,
        ConfirmationWindowView view,
        IWindowCloseMediator closeMediator) 
        : base(model, view, closeMediator)
    {
    }

    public override async Task ConstructWindow()
    {
        View.SetMessage(Model.Message);
        View.CloseButtonClick += CloseMediator.RequestCloseTopWindow;
    }
}
```

## 📁 Расположение префабов

Указывается через `IPathWindowProvider`:

```csharp
_map[typeof(PreviewPuzzleWindow)] = "windows/PreviewPuzzleWindow";
```

## 🔁 Работа со стеком окон

- Новое окно добавляется в стек.
- При закрытии текущего окна — предыдущее окно в стеке возвращается в активное состояние.

## 🔄 Расширяемость

- Можно добавлять любые окна без изменения существующего кода.
- Достаточно зарегистрировать путь, модель и презентер в `WindowPathProvider`, `ModelsFactory`, `PresentersFactory`.

## 📌 Требования

- Unity 2021.3+
- Префабы окон должны содержать компоненты `Canvas`, `CanvasScaler`, `GraphicRaycaster`
- Префабы должны реализовывать интерфейс `IWindow`

## 📦 Зависимости

- **UnityEngine.UI**
- **System.Threading.Tasks**
- (Опционально) **Unity Addressables или Zenject** для продвинутого управления зависимостями

## 💬 Обратная связь

Если у вас есть идеи по улучшению архитектуры или вы нашли баг, пожалуйста, создайте [Issue](https://github.com/flexer1992/simple-window-system) или отправьте PR.

## 📝 Лицензия

Этот проект распространяется под лицензией MIT.