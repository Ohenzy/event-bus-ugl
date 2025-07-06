using UGL.Event;
using UnityEngine;

namespace Example
{
    public class MessagePublisher : MonoBehaviour
    {
        [SerializeField] private EventBusMono eventBus;

        private int _count;
        
        private void Start()
        {
            eventBus.Invoke(new MessageEvent("Hello Event!"));
        }

        private void Update()
        {
            eventBus.Invoke(new CountEvent(_count++));
        }
    }
}