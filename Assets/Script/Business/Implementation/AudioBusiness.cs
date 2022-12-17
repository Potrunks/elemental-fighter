using Assets.Script.Business.Interface;
using Assets.Script.Data.Reference;
using Assets.Script.Entities.Audio;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    public class AudioBusiness : IAudioBusiness
    {
        public IDictionary<SoundEffectType, List<AudioSource>> CreateAudioSourceListBySoundEffectType(List<SoundEffect> soundEffectList, GameObject gameObjectToAddAudioSource)
        {
            Debug.Log("Start of -> Class : " + nameof(AudioBusiness) + " -> Method : " + nameof(AudioBusiness.CreateAudioSourceListBySoundEffectType));
            Debug.Log(soundEffectList.Count() + " sound effect in total");
            IDictionary<SoundEffectType, List<AudioSource>> audioSourceListByType = null;
            if (soundEffectList.Any())
            {
                audioSourceListByType = new Dictionary<SoundEffectType, List<AudioSource>>();
                IEnumerable<SoundEffectType> existingSoundEffectTypeList = soundEffectList.Select(sound => sound.Type).Distinct();
                Debug.Log(existingSoundEffectTypeList.Count() + " sound effect type existing in sound effect list");
                foreach (SoundEffectType type in existingSoundEffectTypeList)
                {
                    audioSourceListByType.Add(type, new List<AudioSource>());
                    foreach (SoundEffect sound in soundEffectList.Where(sound => sound.Type == type))
                    {
                        AudioSource audioSource = gameObjectToAddAudioSource.AddComponent<AudioSource>();
                        audioSource.clip = sound.File;
                        audioSource.volume = (GameManager.instance != null ? GameManager.instance.volumeMainTheme : 0.5f) * sound.AmplifyVolumeValue;
                        audioSource.pitch = sound.Speed;
                        audioSource.loop = sound.IsLooping;
                        audioSource.time = sound.StartedTime;
                        audioSourceListByType[type].Add(audioSource);
                    }
                }
                Debug.Log(audioSourceListByType.Keys.Count() + " sound effect type in the dictionnary and " + audioSourceListByType.Values.Count() + " sound effect in the dictionnary");
            }
            Debug.Log("End of -> Class : " + nameof(AudioBusiness) + " -> Method : " + nameof(AudioBusiness.CreateAudioSourceListBySoundEffectType));
            return audioSourceListByType;
        }

        private void PlayRandomAudioSource(List<AudioSource> audioSourceList)
        {
            if (audioSourceList != null && audioSourceList.Any())
            {
                System.Random random = new System.Random();
                int randomInteger = random.Next(0, audioSourceList.Count);
                AudioSource randomAudioSource = audioSourceList[randomInteger];
                randomAudioSource.Play();
            }
        }

        public void PlayRandomSoundEffect(SoundEffectType soundEffectTypeToPlay, IDictionary<SoundEffectType, List<AudioSource>> soundEffectListByType)
        {
            List<AudioSource> audioSourceList = soundEffectListByType != null ? soundEffectListByType[soundEffectTypeToPlay] : null;
            PlayRandomAudioSource(audioSourceList);
        }
    }
}
