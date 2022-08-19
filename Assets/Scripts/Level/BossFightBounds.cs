using UnityEngine;

namespace Beehaw.Level
{
    public class BossFightBounds : MonoBehaviour
    {
        private bool isBossFightStarted;
        private Collider2D collider;
        

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        public float getMinX()
        {
            return collider.bounds.center.x - collider.bounds.extents.x;
        }

        public float getMaxX()
        {
            return collider.bounds.center.x + collider.bounds.extents.x;
        }

        public bool IsBossFightStarted()
        {
            return isBossFightStarted;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isBossFightStarted = true;
            }
        }
    }
}