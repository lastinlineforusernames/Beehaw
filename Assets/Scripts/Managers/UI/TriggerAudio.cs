using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public void PlayClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }
}
