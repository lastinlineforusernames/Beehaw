using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Managers
{
    public class Singleton : MonoBehaviour
    {
        public static EventSystem eventSystem;

        private void Awake()
        {
            if (eventSystem != null && eventSystem != this.GetComponent<EventSystem>())
            {
                Destroy(gameObject);
            }
            else
            {
                eventSystem = this.GetComponent<EventSystem>();
            }
        }
    }
}