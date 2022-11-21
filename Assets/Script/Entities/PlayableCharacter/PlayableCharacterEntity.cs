using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Entities
{
    [CreateAssetMenu(menuName = "Playable Character/New")]
    public  class PlayableCharacterEntity : ScriptableObject
    {
        public GameObject model;
        public float moveSpeed;
        public List<Sound> soundEffectList;
    }
}
