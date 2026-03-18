# ⚔️ ECS Battle Simulator — Учебный проект OTUS

![Unity](https://img.shields.io/badge/Unity-2021.3.7f1-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-.NET-239120?logo=csharp)
![ECS](https://img.shields.io/badge/Architecture-ECS-blueviolet)
![Framework](https://img.shields.io/badge/Framework-LeoEcsLite-blue)
![Course](https://img.shields.io/badge/Course-OTUS-orange)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)

> Учебный проект курса **OTUS «Разработчик игр на Unity»**.  
> В рамках проекта изучалась архитектура **ECS (Entity–Component–System)** и её применение для построения масштабируемой игровой логики.

---

## 🎮 О проекте

**ECS Battle Simulator** — это мини-игра в жанре **автоматической битвы (auto-battler)**, где два отряда юнитов (Лучники и Мечники) сражаются друг с другом в режиме реального времени без участия игрока.

### Ключевая цель проекта

Освоить архитектурный паттерн **ECS** на практике с применением библиотеки [Leopotam EcsLite](https://github.com/Leopotam/ecslite) — одного из наиболее популярных ECS-фреймворков в Unity-разработке.

---

## 🕹️ Игровая механика

| Механика | Описание |
|---|---|
| ⚔️ **Автоматический бой** | Юниты самостоятельно ищут ближайшего врага и атакуют его |
| 🏹 **Классы юнитов** | Лучники (дистанционная атака стрелами) и Мечники (ближний бой) |
| ❤️ **Система здоровья** | У каждого юнита и базы есть шкала HP с визуальным индикатором |
| 🔄 **Возрождение** | Погибшие юниты автоматически воскрешаются через некоторое время |
| 🏰 **Базы** | Каждая команда имеет базу; первая разрушенная база проигрывает |
| 🏆 **Конец игры** | При уничтожении вражеской базы отображается экран победителя |
| ♻️ **Object Pooling** | Стрелы и юниты переиспользуются через пул объектов |

---

## 🏗️ Архитектура ECS

### Что такое ECS?

**ECS (Entity–Component–System)** — архитектурный паттерн, популярный в геймдеве, где:

- **Entity (Сущность)** — просто уникальный ID; сама по себе не содержит данных или логики.
- **Component (Компонент)** — чистая структура данных, описывающая одно свойство сущности.
- **System (Система)** — логика, обрабатывающая все сущности, обладающие нужным набором компонентов.

Преимущества подхода: высокая производительность за счёт cache-friendly данных, лёгкое масштабирование, чёткое разделение ответственности.

---

### 🧩 Компоненты (Components)

Компоненты — это структуры данных без логики. Примеры из проекта:

```
Components/
├── Position.cs            — позиция в мировом пространстве
├── Health.cs              — максимальное здоровье
├── HealthCurrent.cs       — текущее здоровье
├── Team.cs                — принадлежность к команде (0 / 1)
├── MoveSpeed.cs           — скорость перемещения
├── MoveDirection.cs       — направление движения
├── AttackDistance.cs      — радиус атаки
├── AttackDamage.cs        — урон от атаки
├── AttackTimeOut.cs       — интервал между атаками
├── TargetEntity.cs        — ссылка на цель
├── LifeTime.cs            — время жизни (для стрел)
├── Tags/                  — теги-маркеры: UnitTag, BaseTag, DeadTag, …
├── Events/                — события: AttackEvent, DamageEvent, DeathEvent, …
└── Requests/              — запросы: AttackRequest, FireRequest, DeathRequest, …
```

---

### ⚙️ Системы (Systems)

Системы реализуют всю игровую логику, подписываясь на нужные наборы компонентов через фильтры:

```
Systems/
├── TargetSystem               — поиск ближайшего врага
├── AttackDistanceCheckSystem  — проверка дальности атаки
├── AttackTimeoutSystem        — отсчёт таймера атаки
├── AttackRequestSystem        — запрос начала атаки
├── AttackStartAttackSystem    — запуск анимации атаки
├── AttackActionSystem         — выполнение действия атаки
├── AttackFireRequestSystem    — запрос выпуска стрелы
├── SpawnArrowSystem           — спавн стрелы из пула
├── RespawnArrowSystem         — возврат стрелы в пул
├── ArrowHitSystem             — определение попадания стрелы
├── MoveDirectionSetSystem     — вычисление направления движения
├── MovementSystem             — перемещение юнитов
├── DealDamageSystem           — применение урона
├── DeathRequestSystem         — запрос на смерть
├── DeathSystem                — обработка смерти
├── HealthBarSystem            — обновление шкалы HP
├── UnitRespawnSystem          — возрождение юнитов
├── UnitsSpawnTimeoutSystem    — таймер спавна
├── UnitsSpawnSystem           — спавн юнитов из базы
├── ArrowLifeTimeCheckSystem   — удаление устаревших стрел
├── DestroyBaseSystem          — разрушение базы
└── View/                      — визуальные системы (Transform, Animator, Audio, VFX, UI)
```

---

### 🔄 Конвейер обработки (Pipeline)

```
Каждый кадр (Update):

[TargetSystem] → ищет ближайшего врага
     ↓
[AttackDistanceCheckSystem] → в зоне атаки?
     ↓ Да
[AttackTimeoutSystem] → таймер готов?
     ↓ Да
[AttackRequestSystem] → добавляет AttackRequest
     ↓
[AttackStartAttackSystem] → начинает анимацию
     ↓
[AttackActionSystem] → наносит урон (ближний) / добавляет FireRequest (дальний)
     ↓
[SpawnArrowSystem] → создаёт стрелу из пула
     ↓
[ArrowHitSystem] → стрела достигла цели?
     ↓ Да
[DealDamageSystem] → добавляет DamageEvent → снимает HP
     ↓
[DeathRequestSystem] → HP <= 0? → добавляет DeathRequest
     ↓
[DeathSystem] → помечает DeadTag, запускает DeathEvent
     ↓
[UnitRespawnSystem] → таймер → возрождение
```

---

### 📦 Паттерны, применённые в проекте

| Паттерн | Описание | Применение |
|---|---|---|
| **Event Pattern** | Однокадровые компоненты-события | `AttackEvent`, `DamageEvent`, `DeathEvent` |
| **Request Pattern** | Компоненты-запросы для связи систем | `AttackRequest`, `FireRequest`, `DeathRequest` |
| **Object Pooling** | Переиспользование объектов | Стрелы, юниты — через пул |
| **Tag Components** | Теги для фильтрации сущностей | `DeadTag`, `InactiveTag`, `UnitTag` |
| **Timeout Pattern** | Счётчики обратного отсчёта | Таймеры атаки и спавна |

---

## 🛠️ Технологии

| Инструмент | Версия | Назначение |
|---|---|---|
| [Unity](https://unity.com/) | 2021.3.7f1 LTS | Игровой движок |
| [Leopotam EcsLite](https://github.com/Leopotam/ecslite) | 2023.11.22 | ECS-фреймворк |
| [EcsLite.Di](https://github.com/Leopotam/ecslite-di) | 2023.4.22 | Dependency Injection для ECS |
| [EcsLite.ExtendedSystems](https://github.com/Leopotam/ecslite-extendedsystems) | 2023.1.22 | Расширения систем (DelHere и др.) |
| [EcsLite.Entities](https://github.com/Leopotam/ecslite-entities) | — | Привязка компонентов к GameObject |
| [EcsLite.UnityEditor](https://github.com/Leopotam/ecslite-unityeditor) | 2023.9.22 | Debug-инструменты для ECS в редакторе |
| C# / .NET | — | Язык программирования |
| TextMesh Pro | — | Рендеринг UI-текста |

---

## 📁 Структура проекта

```
Assets/
├── Scripts/
│   ├── EcsEngine/
│   │   ├── EcsStartup.cs          — точка входа, создаёт мир и регистрирует системы
│   │   ├── Components/            — все компоненты (данные)
│   │   ├── Systems/               — вся игровая логика
│   │   │   └── View/              — визуальные системы (анимации, звук, VFX)
│   │   ├── Views/                 — MonoBehaviour-обёртки для ECS
│   │   └── Services/              — вспомогательные сервисы
│   ├── Content/
│   │   ├── ArcherInstaller.cs     — настройка сущности «Лучник»
│   │   ├── SwordsmanInstaller.cs  — настройка сущности «Мечник»
│   │   ├── BaseInstaller.cs       — настройка сущности «База»
│   │   ├── ArrowInstaller.cs      — настройка сущности «Стрела»
│   │   └── UiInstaller.cs         — настройка UI
│   └── GameSystem/
│       └── SpawnController.cs     — контроллер спавна
├── Scenes/
│   └── Scene.unity                — основная игровая сцена
├── Prefabs/                       — префабы юнитов, базы, стрелы, HP-бара
├── Audio/                         — звуковые эффекты
└── Plugins/                       — ECS-библиотеки
```

---

## 🚀 Запуск проекта

### Требования

- [Unity Hub](https://unity.com/download)
- Unity **2021.3.7f1 LTS**

### Шаги

1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/MrRandomise/ECSHomeWork.git
   ```
2. Откройте Unity Hub → **Add project from disk** → выберите папку `ECSHomeWork`.
3. Убедитесь, что используется версия Unity **2021.3.7f1**.
4. Откройте сцену: `Assets/Scenes/Scene.unity`.
5. Нажмите **Play** в редакторе Unity.

---

## 📚 Чему я научился

В ходе выполнения проекта были изучены и применены на практике:

- ✅ Основы архитектуры **ECS**: разделение данных и логики
- ✅ Работа с **Leopotam EcsLite**: создание мира, систем, пулов компонентов
- ✅ Использование **фильтров** (`Inc<>` / `Exc<>`) для точного выбора сущностей
- ✅ **Dependency Injection** в ECS через `EcsPoolInject` и `EcsFilterInject`
- ✅ **Event-driven** коммуникация между системами без прямых зависимостей
- ✅ **Request-Response** паттерн для разделения намерения и исполнения
- ✅ **Object Pooling** для снижения нагрузки на GC
- ✅ Интеграция ECS с Unity: синхронизация `Transform`, `Animator`, аудио и VFX
- ✅ Отладка ECS-мира с помощью **EcsWorldDebugSystem**

---

## 👤 Автор

Проект выполнен в рамках обучения на курсе **«Разработчик игр на Unity»** платформы [OTUS](https://otus.ru/).

---

## 📄 Лицензия

Проект создан в учебных целях. Все права на используемые ассеты принадлежат их авторам.
