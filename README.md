# 🎮 Survival-Top-Down

Một game **3D Action/MOBA Prototype** được phát triển bằng Unity, lấy cảm hứng từ các game MOBA như **Liên Quân Mobile**.

Project tập trung vào việc xây dựng hệ thống gameplay có tính module, bao gồm điều khiển nhân vật, combat, kỹ năng, Enemy AI, Object Pooling, Observer Pattern và hệ thống âm thanh.

Sử dụng Unity 6000.3.5f2 LTS 

---

## 🎯 Tổng quan

Người chơi điều khiển một nhân vật có **3 kỹ năng chính**:

| Kỹ năng        | Mô tả                                        |
| -------------- | -------------------------------------------- |
| ⚔️ Đánh thường | Đòn tấn công cơ bản                          |
| 💨 Dash        | Lướt nhanh theo hướng chỉ định               |
| 💣 Bomb        | Ném một quả bom/projectile về hướng mục tiêu |

Game hỗ trợ cả **Mobile** và **PC**.

### 📱 Mobile

* Joystick ảo để di chuyển.
* Button UI để sử dụng kỹ năng.

### 💻 PC

* `W` / `A` / `S` / `D` để di chuyển.
* Keyboard / Mouse cho các thao tác gameplay.

---

# ⚔️ Hệ thống Combat

Combat system được xây dựng theo hướng các kỹ năng có logic riêng và có thể tương tác với những hệ thống gameplay khác.

```text
Player
 ├── Basic Attack
 ├── Dash
 └── Bomb
```

Các hệ thống combat đã triển khai / thử nghiệm:

* Đánh thường.
* Dash.
* Projectile.
* Bomb.
* Directional Projectile.
* Projectile không tự động đuổi theo mục tiêu.
* Cone-shaped Attack.
* Hit Detection.
* Damage.
* Attack Animation.
* Attack Timing.
* Combat Feedback.

---

# 🎮 Hệ thống điều khiển

Game hỗ trợ hai phương thức điều khiển.

## Mobile

```text
Virtual Joystick
       ↓
Movement Input
       ↓
Player Controller
       ↓
Character Movement
```

## PC

```text
W / A / S / D
       ↓
Movement Input
       ↓
Player Controller
       ↓
Character Movement
```

Việc tách input khỏi movement logic giúp cùng một hệ thống nhân vật có thể hoạt động trên nhiều nền tảng.

---

# 🤖 Enemy AI - Behaviour Tree

Enemy sử dụng **Behaviour Tree** để xử lý hành vi.

Cấu trúc đơn giản:

```text
Enemy
  │
  ▼
Selector
 ├── Attack Player
 ├── Chase Player
 └── Idle
```

Các thành phần Behaviour Tree được sử dụng:

* `Node`
* `ConditionNode`
* `SelectorNode`
* Action Node
* `SUCCESS`
* `FAILURE`
* `RUNNING`

Một vấn đề quan trọng trong quá trình phát triển là xử lý những action cần tồn tại trong nhiều frame.

Ví dụ với Attack:

```text
Attack
  ↓
Play Attack Animation
  ↓
Wait for Attack Timing
  ↓
Apply Damage
  ↓
Finish Attack
  ↓
Return SUCCESS
```

Điều này giúp tránh trường hợp `AttackPlayer()` trả về `SUCCESS` quá sớm khiến animation chưa kịp hiển thị hoặc gameplay logic chưa hoàn thành.

---

# ♻️ Object Pooling

Project sử dụng **Object Pooling** cho những object được spawn thường xuyên.

Ví dụ:

* Projectile.
* Bomb.
* Attack Effect.
* VFX.
* Các gameplay object tồn tại trong thời gian ngắn.

Thay vì liên tục:

```text
Instantiate
     ↓
Use
     ↓
Destroy
```

project sử dụng:

```text
Object Pool
     ↓
Get Object
     ↓
Use Object
     ↓
Return Object
     ↓
Object Pool
```

Mục đích:

* Giảm số lần `Instantiate()`.
* Giảm số lần `Destroy()`.
* Giảm Garbage Collection.
* Cải thiện hiệu năng khi có nhiều object được spawn liên tục.
* Có thể tái sử dụng projectile và effect.

---

# 👀 Observer Pattern

Project sử dụng **Observer Pattern** để các hệ thống có thể giao tiếp với nhau thông qua Event.

Kiến trúc cơ bản:

```text
Gameplay Event
      │
      ├──────────► Player / Gameplay
      │
      ├──────────► Audio
      │
      ├──────────► VFX
      │
      └──────────► UI
```

Ví dụ khi Player nhận damage:

```text
Damage Event
     │
     ├── Update HP
     ├── Play Hit Sound
     ├── Trigger Damage Flash
     └── Trigger VFX
```

Điều này giúp giảm sự phụ thuộc trực tiếp giữa các hệ thống.

Ví dụ:

```text
Damage System
      │
      │ Event
      ▼
 ┌─────────────┐
 │  Observers  │
 ├─────────────┤
 │ UI          │
 │ Audio       │
 │ VFX         │
 │ Animation   │
 └─────────────┘
```

---

# 🔊 Audio System

Project sử dụng **Unity AudioSource** để xử lý âm thanh trong game.

Các loại âm thanh bao gồm:

* Attack Sound.
* Skill Sound.
* Hit Sound.
* Character Voice.
* Victory Music.
* Gameplay Event Sound.

Ngoài ra có sử dụng Random AudioClip cho những action được thực hiện nhiều lần.

Ví dụ:

```csharp
AudioClip clip = clips[Random.Range(0, clips.Length)];
audioSource.PlayOneShot(clip);
```

Việc random audio giúp tránh cảm giác âm thanh bị lặp lại liên tục khi thực hiện cùng một hành động.

---

# 🧱 Architecture

Project áp dụng một số programming pattern và architecture để tổ chức gameplay system.

| Pattern / System               | Mục đích                                           |
| ------------------------------ | -------------------------------------------------- |
| **Observer Pattern**           | Giao tiếp giữa các gameplay system thông qua Event |
| **Behaviour Tree**             | Xây dựng Enemy AI                                  |
| **Object Pooling**             | Tái sử dụng các object được spawn thường xuyên     |
| **Component-based Design**     | Tận dụng kiến trúc Component của Unity             |
| **Event-driven Communication** | Phản hồi gameplay event giữa các system            |

Kiến trúc tổng quan:

```text
                    ┌──────────────┐
                    │    PLAYER    │
                    └──────┬───────┘
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
        Basic Attack      Dash         Bomb
              │            │            │
              └────────────┼────────────┘
                           ▼
                    Gameplay Events
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
            Audio          VFX          UI


                    ┌──────────────┐
                    │    ENEMY     │
                    └──────┬───────┘
                           ▼
                    Behaviour Tree
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
            Attack        Chase         Idle
```

---

# 🛠️ Công nghệ sử dụng

| Công nghệ            | Sử dụng                             |
| -------------------- | ----------------------------------- |
| **Unity**            | Game Engine                         |
| **C#**               | Gameplay Programming                |
| **Unity Physics**    | Collision / Trigger / Hit Detection |
| **Unity Animator**   | Character Animation                 |
| **AudioSource**      | Gameplay Audio                      |
| **Behaviour Tree**   | Enemy AI                            |
| **Observer Pattern** | Event Communication                 |
| **Object Pooling**   | Object Reuse / Performance          |
| **Shader**           | Gameplay Visual Effects             |
| **Mobile Joystick**  | Mobile Character Control            |

---

# 📋 Các hệ thống chính

| Hệ thống          | Trạng thái         |
| ----------------- | ------------------ |
| Player Movement   | ✅                  |
| Mobile Joystick   | ✅                  |
| PC WASD Control   | ✅                  |
| Basic Attack      | ✅                  |
| Dash Skill        | ✅                  |
| Bomb Skill        | ✅                  |
| Projectile System | ✅                  |
| Enemy AI          | ✅                  |
| Behaviour Tree    | ✅                  |
| Object Pooling    | ✅                  |
| Observer Pattern  | ✅                  |
| AudioSource       | ✅                  |
| Attack Animation  | ✅                  |
| Hit Detection     | ✅                  |
| Camera Shake      | 🔧 Đang phát triển |
| Damage Flash      | 🔧 Đang phát triển |
| Hit VFX           | 🔧 Đang phát triển |

---

# 📚 Những gì tôi học được từ project

Project này được xây dựng với mục tiêu thực hành **Unity Gameplay Programming** thông qua một sản phẩm có gameplay tương đối hoàn chỉnh.

Các kiến thức chính:

* Xây dựng Character Controller.
* Xử lý input cho Mobile và PC.
* Xây dựng Ability System.
* Xử lý projectile và collision.
* Xây dựng Enemy AI.
* Sử dụng Behaviour Tree.
* Đồng bộ gameplay logic với animation.
* Sử dụng Observer Pattern.
* Sử dụng Object Pooling.
* Xử lý AudioSource và AudioClip.
* Xây dựng gameplay feedback.
* Debug các vấn đề liên quan đến Collider, Trigger và Rigidbody.
* Tổ chức gameplay code theo hướng module và có thể mở rộng.

---

# 🚧 Hướng phát triển

Một số hướng phát triển tiếp theo:

* [ ] Hoàn thiện Camera Shake.
* [ ] Hoàn thiện Damage Flash.
* [ ] Hoàn thiện Hit VFX.
* [ ] Thêm nhiều Enemy type.
* [ ] Thêm nhiều Ability.
* [ ] Cải thiện Enemy AI.
* [ ] Cải thiện Combat Feedback.
* [ ] Tối ưu Object Pooling.
* [ ] Cải thiện gameplay balancing.
* [ ] Thêm Multiplayer.

---

# 🎯 Mục tiêu của Project

Mục tiêu chính của project là thực hành việc xây dựng một game Unity có gameplay hoàn chỉnh, đồng thời áp dụng các kiến thức về **Game Programming và Software Architecture** vào những vấn đề thực tế.

Thay vì chỉ tập trung làm cho game "chạy được", project hướng tới việc xây dựng các system:

```text
Functional
    ↓
Modular
    ↓
Reusable
    ↓
Maintainable
    ↓
Easy to Extend
```

> **Build → Break → Debug → Understand → Improve.**

---

## 👨‍💻 Developer

**Unity / C# Game Developer**

Tập trung vào:

* Gameplay Programming
* Combat System
* Enemy AI
* Behaviour Tree
* Game Architecture
* Unity Optimization
