using System.Collections;
using UnityEngine;

namespace Beehaw.Character
{
    public class WasperadoJump : CharacterJump
    {
        protected override void PlayJumpAudio()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/WaspJump");
        }
    }
}