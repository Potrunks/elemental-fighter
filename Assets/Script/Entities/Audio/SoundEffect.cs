using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.Entities.Audio
{
    [CreateAssetMenu(menuName = "Audio/New Sound Effect")]
    public class SoundEffect : Audio
    {
        [field : SerializeField]
        public SoundEffectType Type { get; private set; }
    }
}
