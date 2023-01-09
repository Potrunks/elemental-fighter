using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.Entities.Audio
{
    [CreateAssetMenu(menuName = "Audio/New Voice")]
    public class Voice : Audio
    {
        [field: SerializeField]
        public VoiceType Type { get; private set; }
    }
}
