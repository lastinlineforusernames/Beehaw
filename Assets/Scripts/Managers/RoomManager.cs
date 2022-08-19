using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera;
    [SerializeField] private bool showBounds;
    [SerializeField] private GameObject player;
    private PolygonCollider2D bounds;

    private void Awake()
    {
        bounds = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (showBounds)
        {
            Vector2[] points = bounds.points;
            Gizmos.color = Color.magenta;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(transform.TransformPoint(points[i]), transform.TransformPoint(points[i + 1]));
            }
            Gizmos.DrawLine(transform.TransformPoint(points[points.Length - 1]), transform.TransformPoint(points[0]));
        }
    }
}
