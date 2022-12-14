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
            IDictionary<SoundEffectType, List<AudioSource>> audioSourceListByType = null;
            if (soundEffectList.Any())
            {
                audioSourceListByType = new Dictionary<SoundEffectType, List<AudioSource>>();
                IEnumerable<SoundEffectType> existingSoundEffectTypeList = soundEffectList.Select(sound => sound.Type).Distinct();
                foreach (SoundEffectType type in existingSoundEffectTypeList)
                {
                    audioSourceListByType.Add(type, new List<AudioSource>());
                    foreach (SoundEffect sound in soundEffectList.Where(sound => sound.Type == type))
                    {
                        AudioSource audioSource = gameObjectToAddAudioSource.AddComponent<AudioSource>();
                        audioSource.clip = sound.File;
                        audioSource.volume = GameManager.instance.volumeMainTheme * sound.AmplifyVolumeValue;
                        audioSource.pitch = sound.Speed;
                        audioSource.loop = sound.IsLooping;
                        audioSource.time = sound.StartedTime;
                        audioSourceListByType[type].Add(audioSource);
                    }
                }
            }
            return audioSourceListByType;
        }
    }
}
