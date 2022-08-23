using System;
using System.Collections;
using UnityEngine;

namespace Beehaw.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager audioManager;
        private const string titleBGM = "event:/BGM_Title";
        private const string gameBGM = "event:/BGM_HiveHills";
        private const string gameOverBGM = "event:/BGM_GameOver";
        private const string bossFightBGM = "event:/BGM_BossFight";
        private const string victoryBGM = "event:/BGM_Victory";
        private FMOD.Studio.EventInstance music;

        private void Awake()
        {
            if (audioManager != null && audioManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                audioManager = this;
                DontDestroyOnLoad(audioManager);
            }
        }

        public void playTitleBGM()
        {
            string musicEvent = titleBGM;
            PlayMusic(musicEvent);
        }

        public void playGameBGM()
        {
            string musicEvent = gameBGM;
            PlayMusic(musicEvent);
        }

        public void playGameOverBGM()
        {
            string musicEvent = gameOverBGM;
            PlayMusic(musicEvent);
        }

        public void playBossFightBGM()
        {
            string musicEvent = bossFightBGM;
            PlayMusic(musicEvent);
        }

        public void playVictoryBGM()
        {
            string musicEvent = victoryBGM;
            PlayMusic(musicEvent);
        }

        private void PlayMusic(string musicEvent)
        {
            music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            music = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
            music.start();
        }
    }
}