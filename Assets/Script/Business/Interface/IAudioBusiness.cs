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

        public AudioSource PlayRandomSoundEffect(SoundEffectType soundEffectTypeToPlay, IDictionary<SoundEffectType, List<AudioSource>> soundEffectListByType);
    }
}
