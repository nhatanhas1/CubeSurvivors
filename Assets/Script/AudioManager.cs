using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager.Sound;

public class AudioManager : MonoBehaviour
{        
    [System.Serializable]
    public class Sound
    {
        public string name;

        public enum SoundName
        {
            AxeAttack,
            KnifeAttack,
            FireAttack,
            GetExp,
            Background,
            LevelUp,
            CrossAttack,
            StartTheme,
            GameOver,
        }

        public SoundName _SoundName;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource audioSource;

        public bool isLoop;
        public bool isPlayOnAwake;
    }

    public Sound[] sounds;

    public static AudioManager Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.isLoop;
            s.audioSource.playOnAwake = s.isPlayOnAwake;
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(SoundName soundName )
    {
        Sound s = Array.Find(sounds, sound => sound._SoundName == soundName );
        s.audioSource.Play();
    }

    public AudioSource GetAudioSource(SoundName soundName)
    {
        Sound s = Array.Find(sounds, sound => sound._SoundName == soundName);
        return s.audioSource;
    }
   
}
