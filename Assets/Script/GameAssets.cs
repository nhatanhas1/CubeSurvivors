using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance
    {
        get {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return instance; 
        }
    }


    public SoundAudioClip[] SoundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        //public AudioSource audioSource; 
        public AudioClip audioClip;
    }
}
