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
                        audioSource.volume = (GameManager.instance != null ? GameManager.instance.volumeMainTheme : SettingValueReference.MAIN_VOLUME_DEFAULT) * sound.AmplifyVolumeValue;
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

        private AudioSource PlayRandomAudioSource(List<AudioSource> audioSourceList)
        {
            AudioSource randomAudioSource = null;
            if (audioSourceList != null && audioSourceList.Any())
            {
                System.Random random = new System.Random();
                int randomInteger = random.Next(0, audioSourceList.Count);
                randomAudioSource = audioSourceList[randomInteger];
                randomAudioSource.Play();
            }
            return randomAudioSource;
        }

        public AudioSource PlayRandomSoundEffect(SoundEffectType soundEffectTypeToPlay, IDictionary<SoundEffectType, List<AudioSource>> soundEffectListByType)
        {
            List<AudioSource> audioSourceList = soundEffectListByType.ContainsKey(soundEffectTypeToPlay) ? soundEffectListByType[soundEffectTypeToPlay] : null;
            AudioSource audioSourcePlayed = PlayRandomAudioSource(audioSourceList);
            return audioSourcePlayed;
        }

        public IDictionary<VoiceType, List<AudioSource>> CreateAudioSourceListByVoiceType(List<Voice> voiceList, GameObject gameObjectToAddAudioSource)
        {
            Debug.Log("Start of -> Class : " + nameof(AudioBusiness) + " -> Method : " + nameof(AudioBusiness.CreateAudioSourceListByVoiceType));
            Debug.Log(voiceList.Count() + " voice in total");
            IDictionary<VoiceType, List<AudioSource>> audioSourceListByType = null;
            if (voiceList.Any())
            {
                audioSourceListByType = new Dictionary<VoiceType, List<AudioSource>>();
                IEnumerable<VoiceType> existingVoiceTypeList = voiceList.Select(voice => voice.Type).Distinct();
                Debug.Log(existingVoiceTypeList.Count() + " voice type existing in voice list");
                foreach (VoiceType type in existingVoiceTypeList)
                {
                    audioSourceListByType.Add(type, new List<AudioSource>());
                    foreach (Voice voice in voiceList.Where(voice => voice.Type == type))
                    {
                        AudioSource audioSource = gameObjectToAddAudioSource.AddComponent<AudioSource>();
                        audioSource.clip = voice.File;
                        audioSource.volume = (GameManager.instance != null ? GameManager.instance.volumeMainTheme : SettingValueReference.MAIN_VOLUME_DEFAULT) * voice.AmplifyVolumeValue;
                        audioSource.pitch = voice.Speed;
                        audioSource.loop = voice.IsLooping;
                        audioSource.time = voice.StartedTime;
                        audioSourceListByType[type].Add(audioSource);
                    }
                }
                Debug.Log(audioSourceListByType.Keys.Count() + " voice type in the dictionnary and " + audioSourceListByType.Values.Count() + " voice in the dictionnary");
            }
            Debug.Log("End of -> Class : " + nameof(AudioBusiness) + " -> Method : " + nameof(AudioBusiness.CreateAudioSourceListByVoiceType));
            return audioSourceListByType;
        }

        public AudioSource PlayRandomVoice(VoiceType voiceTypeToPlay, IDictionary<VoiceType, List<AudioSource>> voiceListByType)
        {
            List<AudioSource> audioSourceList = voiceListByType.ContainsKey(voiceTypeToPlay) ? voiceListByType[voiceTypeToPlay] : null;
            AudioSource audioSourcePlayed = PlayRandomAudioSource(audioSourceList);
            return audioSourcePlayed;
        }
    }
}
