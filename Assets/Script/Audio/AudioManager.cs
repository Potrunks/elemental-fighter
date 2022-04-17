using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* If needed, do a singleton pattern with this instance
    public static AudioManager instance;
    */
    public Sound[] sounds;
    void Awake()
    {
        /* Implementation of singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        */
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.time = sound.time;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound sound = FindSoundByName(name);
        if (sound != null)
        {
            sound.audioSource.Play();
        }
    }
    public void Stop(string name)
    {
        Sound sound = FindSoundByName(name);
        if (sound != null)
        {
            sound.audioSource.Stop();
        }
    }

    private Sound FindSoundByName(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("The sound named " + name + " is not found in the AudioManager.");
            return sound;
        }
        return sound;
    }
}