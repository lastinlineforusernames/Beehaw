using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class WasperadoMovement : CharacterMovement
    {
        private GameObject player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void FlipCharacter()
        {
            if (player != null)
            {
                transform.localScale = new Vector3(player.transform.position.x > transform.position.x ? 1 : -1, 1, 1);
            }
            base.FlipCharacter();
        }
    }
}