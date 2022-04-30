using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* If needed, do a singleton pattern with this instance
    public static AudioManager instance;
    */
    public Sound[] sounds;
    private GameManager gameManager;
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
        gameManager = GameManager.instance;
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = gameManager.volumeMainTheme;
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

    public void DecreaseVolume(string name, float percentage)
    {
        Sound sound = FindSoundByName(name);
        if (sound != null)
        {
            sound.audioSource.volume = gameManager.volumeMainTheme * percentage / 100;
        }
    }

    public void RestoreOriginVolume(string name)
    {
        Sound sound = FindSoundByName(name);
        if (sound != null)
        {
            sound.audioSource.volume = gameManager.volumeMainTheme;
        }
    }

    public Sound FindSoundByName(string name)
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
