using UnityEngine;
using Cinemachine;

namespace Beehaw.Character
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private float deadZoneY = 0.003853565f;
        private CollisionChecker groundChecker;
        private CinemachineFramingTransposer transposer;

        private void Awake()
        {
            groundChecker = GetComponent<CollisionChecker>();
        }

        private void Start()
        {
            transposer = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void Update()
        {

            if(groundChecker.IsOnGround())
            {
                transposer.m_DeadZoneHeight = 0.08f;
            } 
            else
            {
                transposer.m_DeadZoneHeight = 2f;
            }
        }
    }
}