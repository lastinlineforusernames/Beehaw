using UnityEngine;

namespace Beehaw.Character
{
    public class CharacterController : MonoBehaviour
    {
        protected float horizontalInput;
        protected float verticalInput;
        protected bool isJumpButtonPressed;
        protected bool isJumpButtonReleased;
        protected bool shouldFirePrimary;
        protected bool shouldFireSecondary;

        public float GetHorizontalInput()
        {
            return horizontalInput;
        }

        public float GetVerticalInput()
        {
            return verticalInput;
        }

        public bool IsJumpButtonPressed()
        {
            return isJumpButtonPressed;
        }

        public bool IsJumpButtonReleased()
        {
            return isJumpButtonReleased;
        }

        public bool ShouldFirePrimary()
        {
            return shouldFirePrimary;
        }

        public bool ShouldFireSecondary()
        {
            return shouldFireSecondary;
        }

        protected virtual void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            isJumpButtonPressed = Input.GetButtonDown("Jump");
            isJumpButtonReleased = Input.GetButtonUp("Jump");
            shouldFirePrimary = Input.GetButtonDown("Fire1");
            shouldFireSecondary = Input.GetButtonDown("Fire2");
        }

    }
}