using Assets.Script.Data.Reference;
using Assets.Script.Entities.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Business.Interface
{
    public interface IAudioBusiness
    {
        /// <summary>
        /// Create a dictionary of audio source list by sound effect type from a sound effect list.
        /// All audio source created during this method will be add in the game object given in parameters.
        /// The dictionary created is return.
        /// </summary>
        public IDictionary<SoundEffectType, List<AudioSource>> CreateAudioSourceListBySoundEffectType(List<SoundEffect> soundEffectList, GameObject gameObjectToAddAudioSource);

        /// <summary>
        /// Create a dictionary of audio source list by voice type from a voice list.
        /// All audio source created during this method will be add in the game object given in parameters.
        /// The dictionary created is return.
        /// </summary>
        public IDictionary<VoiceType, List<AudioSource>> CreateAudioSourceListByVoiceType(List<Voice> voiceList, GameObject gameObjectToAddAudioSource);

        /// <summary>
        /// Play a sound effect audio source depending of the type given.
        /// </summary>
        public AudioSource PlayRandomSoundEffect(SoundEffectType soundEffectTypeToPlay, IDictionary<SoundEffectType, List<AudioSource>> soundEffectListByType);

        /// <summary>
        /// Play a voice audio source depending of the type given.
        /// </summary>
        public AudioSource PlayRandomVoice(VoiceType voiceTypeToPlay, IDictionary<VoiceType, List<AudioSource>> voiceListByType);
    }
}
