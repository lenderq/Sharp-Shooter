# Sharp Shooter
<img width="2228" height="1245" alt="image" src="https://github.com/user-attachments/assets/814c2eb4-f88a-4bf9-bf66-3afe73ed74f4" />
<img width="2231" height="1247" alt="image" src="https://github.com/user-attachments/assets/128a1ab8-19ff-437f-aa0f-dec60d40b91e" />
<img width="2233" height="1252" alt="image" src="https://github.com/user-attachments/assets/1b74a803-be3b-48d1-8a92-f0f3360f17a9" />
<img width="2233" height="1251" alt="image" src="https://github.com/user-attachments/assets/462d7131-089b-4469-b31b-4a2ef358347c" />
<img width="2229" height="1245" alt="image" src="https://github.com/user-attachments/assets/ad640a21-5475-4343-98e0-82da7b0878e8" />
<img width="2236" height="1254" alt="image" src="https://github.com/user-attachments/assets/4feb9641-8eb5-4269-87cf-ac908261eb68" />

## Описание проекта

**Sharp Shooter** — это динамичный FPS с системой смены оружия и волновым противником. Игрок управляет персонажем от первого лица, уничтожает роботов и турели, собирает патроны и новое оружие. Победа достигается уничтожением всех врагов, при этом игрок должен следить за здоровьем и боезапасом.

## Ключевые возможности

- **Система оружия**:
  - Смена оружия через пикапы (`WeaponPickup`) с использованием Scriptable Objects (`WeaponSO`).
  - Разные типы: автоматическое/одиночное, прицел (`CanZoom`), отдача (`CinemachineImpulse`).
  - Магазин с перезарядкой (`MagazineSize`), отображение патронов (`ammoText`).
- **Разумные противники**:
  - **Роботы**: Используют NavMeshAgent для преследования игрока (`Robot.cs`).
  - **Турели**: Автоматическая стрельба по игроку с поворотом башни (`Turret.cs`).
  - Общая система здоровья (`EnemyHealth`) с VFX при уничтожении.
- **Игровая механика**:
  - Подсчет оставшихся врагов (`GameManager`).
  - Взрывные робота (`Explosion`) с радиусом поражения.
  - UI здоровья (`shieldBars`) и экран победы/поражения.
- **Визуальные эффекты**:
  - Эффекты из ствола (`muzzleFlash`), попадания (`hitVFX`).
  - Камера смерти (`deathVirtualCamera`) при проигрыше.

## Техническая реализация

- **ActiveWeapon.cs**: Центральный контроллер оружия. Обрабатывает стрельбу (`Raycast`), зум (`fieldOfView`), переключение оружия (`SwitchWeapon`). Интегрируется со Starter Assets FPS Controller.
- **WeaponSO**: Scriptable Object для конфигурации оружия (урон, скорострельность, магазин, зум). Поддерживает автоматический/ручной режим.
- **Enemy AI**:
  - `Robot`: NavMeshAgent следует за игроком, самоуничтожается при контакте.
  - `Turret`: Корутина для периодической стрельбы (`FireRoutine`), поворот башни на игрока.
- **SpawnGate**: Волновый спавнер роботов с задержкой (`spawnDelay`).

## Установка и запуск
Скачивание билда: Перейдите в раздел [Releases](https://github.com/lenderq/Sharp-Shooter/releases/tag/1.0) и скачайте актуальную версию.

**Или**

1. **Клонирование репозитория**:
   ```bash
   https://github.com/lenderq/Sharp-Shooter.git
   ```

2. **Импорт Starter Assets**:
   - Window → Package Manager → Unity Registry.
   - Найти "Starter Assets - First Person Character Controller".
   - Import.

3. **Настройка проекта**:
   - Откройте сцену `GameScene`.
   - Убедитесь, что NavMesh запечен (Window → AI → Navigation).

4. **Запуск**:
   - Play в редакторе Unity.

## Управление

| Действие | Клавиша | Описание |
|----------|---------|----------|
| **Перемещение** | `WASD` | Движение персонажа |
| **Прицеливание** | `Мышь` | Поворот камеры |
| **Стрельба** | `ЛКМ` | Выстрел (авто/одиночный) |
| **Зум** | `ПКМ` | Прицел (для оружия с `CanZoom=true`) |
| **Рестарт** | UI Button | Перезапуск уровня |

## Система оружия (WeaponSO)

| Параметр | Описание |
|----------|----------|
| `Damage` | Урон за выстрел |
| `FireRate` | Задержка между выстрелами |
| `MagazineSize` | Размер магазина |
| `IsAutomatic` | Автоматический режим |
| `CanZoom` | Поддержка прицела |
