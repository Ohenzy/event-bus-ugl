using UGL.Event;
using UnityEngine;
using UnityEngine.Events;

namespace Example
{
    public class MessagePrinter : MonoBehaviour
    {
        [SerializeField] private EventBusMono eventBus;

        private void Awake()
        {
            eventBus.For(this)
                .Subscribe(new UnityAction<MessageEvent>(OnMessage))
                .Subscribe(new UnityAction<CountEvent>(OnCount))
                .Build();
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe(this);
        }
        
        private void OnCount(CountEvent evt)
        {
            print(evt.CountValue);
            
            if (evt.CountValue >= 10)
            {
                Destroy(gameObject);
            }
        }

        private void OnMessage(MessageEvent evt)
        {
            print(evt.Message);
        }
    }
}