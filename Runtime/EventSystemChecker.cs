using UnityEngine;
using UnityEngine.EventSystems;

namespace UScreens
{
    internal static class EventSystemChecker
    {
        private static EventSystem eventSystem;

        public static void TryToFind()
        {
            if (eventSystem != null)
                return;

            eventSystem = Object.FindObjectOfType<EventSystem>();

            if (eventSystem != null)
                return;

            eventSystem = new GameObject(nameof(EventSystem)).AddComponent<EventSystem>();
            eventSystem.gameObject.AddComponent<StandaloneInputModule>();
        }
    }
}