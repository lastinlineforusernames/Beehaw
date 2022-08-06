using UnityEngine;

namespace Beehaw.Level
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField, Range(0, 10f)] private float moveSpeed;
        private float distanceToStartPoint;
        private float distanceToEndPoint;
        private Vector2 targetPosition;

        private void Start()
        {
            targetPosition = endPoint.position;
            transform.position = startPoint.position;
        }

        private void Update()
        {
            distanceToStartPoint = Vector2.Distance(transform.position, startPoint.position);
            distanceToEndPoint = Vector2.Distance(transform.position, endPoint.position);

            if (distanceToStartPoint < 0.1f)
            {
                targetPosition = endPoint.position;
            } 
            else if (distanceToEndPoint < 0.1f)
            {
                targetPosition = startPoint.position;
            }

            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime / moveSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.collider.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.collider.transform.SetParent(null);
            }
        }
    }
}