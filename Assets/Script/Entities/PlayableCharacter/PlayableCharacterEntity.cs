using Assets.Script.Entities.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Entities
{
    [CreateAssetMenu(menuName = "Playable Character/New")]
    public  class PlayableCharacterEntity : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public GameObject Model { get; private set; }

        [field: SerializeField]
        public float MoveSpeed { get; private set; }

        [field: SerializeField]
        public float JumpForce { get; private set; }

        [field: SerializeField]
        public List<SoundEffect> SoundEffectList { get; private set; }

        [field: SerializeField]
        public List<Voice> VoiceList { get; private set; }

        [field: SerializeField]
        public List<PowerEntity> PowerEntityList { get; private set; }

        [field: SerializeField]
        public int AttackForce { get; private set; }

        [field: SerializeField]
        public int MaxHealth { get; private set; }
    }
}
