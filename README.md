# 🚀 Event Bus System
**Lightweight Event System for Unity**  

[![GitHub release](https://img.shields.io/badge/version-v1.2.0-blue)](https://github.com/Ohenzy/event-bus-ugl/releases/tag/v1.2.0)
[![Unity](https://img.shields.io/badge/Unity-black.svg?logo=unity)](https://unity.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

---

## 📦 **Installation**  
### **Via Git URL (Unity Package Manager)**  
1. Open **Package Manager** → **Add package from git URL** → Paste:<br>
```md
https://github.com/Ohenzy/event-bus-ugl.git?path=/Assets/Scripts/Event/#v1.2.0
```
2. Click **Add**.  

## 🛠 **Usage**  
### **1. Subscribe to an Event**  
```csharp
    public class MessagePrinter : MonoBehaviour
    {
        private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus = GetComponent<IEventBus>().For(this)
                .Subscribe(new UnityAction<MessageEvent>(OnPrintMessage))
                .Build();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe(this);
        }
        
        private void OnPrintMessage(MessageEvent evt)
        {
            print(evt.Message);
        }
    }

    public class MessagePublisher : MonoBehaviour
    {
        private IEventBus _eventBus;
        
        private void Start()
        {
            _eventBus.Invoke(new MessageEvent("Hello Event!"));
        }
    }

    public readonly struct MessageEvent
    {
        public readonly string Message;

        public MessageEvent(string message)
        {
            Message = message;
        }
    }
