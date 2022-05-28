using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* If needed, do a singleton pattern with this instance
    public static AudioManager instance;
    */
    [Header("Sound Effect")]
    public Sound[] sounds;
    public Sound[] lightATKSounds;
    public Sound[] hurtSounds;
    public Sound[] astonishmentSounds;
    public Sound[] blockingSounds;
    public Sound[] dieSounds;
    public Sound[] heavyATKSounds;
    public Sound[] insultSounds;
    public Sound[] jumpSounds;
    public Sound[] mediumATKSounds;

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

        List<Sound[]> allSoundTypes = new List<Sound[]>();
        allSoundTypes.Add(sounds);
        allSoundTypes.Add(lightATKSounds);
        allSoundTypes.Add(hurtSounds);
        allSoundTypes.Add(astonishmentSounds);
        allSoundTypes.Add(blockingSounds);
        allSoundTypes.Add(dieSounds);
        allSoundTypes.Add(heavyATKSounds);
        allSoundTypes.Add(insultSounds);
        allSoundTypes.Add(jumpSounds);
        allSoundTypes.Add(mediumATKSounds);

        CreateAllAudioSourceComponentForAllType(allSoundTypes);
    }

    private void CreateAllAudioSourceComponentForAllType(List<Sound[]> soundTypes)
    {
        foreach (Sound[] soundType in soundTypes)
        {
            CreateAudioSourceComponentByType(soundType);
        }
    }

    private void CreateAudioSourceComponentByType(Sound[] soundType)
    {
        foreach (Sound sound in soundType)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = gameManager.volumeMainTheme * sound.amplifyValue;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.time = sound.time;
        }
    }

    private void Start()
    {
        Play("MainTheme");
        StartPlaySoundsMainMenu();
    }

    public void Play(string name)
    {
        Sound sound = FindSoundByName(name);
        if (sound != null)
        {
            sound.audioSource.Play();
        }
    }

    public void PlaySoundByIndexInListOfSound(Sound[] sounds, int index)
    {
        Debug.Log("Playing sound index : " + index + " in sound list : " + sounds);
        Sound soundToPlay = sounds[index];
        if (soundToPlay != null)
        {
            soundToPlay.audioSource.Play();
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

    private void StartPlaySoundsMainMenu()
    {
        Play("MainTitle");
    }
}
