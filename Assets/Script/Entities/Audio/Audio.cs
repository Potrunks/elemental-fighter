using UnityEngine;

namespace Assets.Script.Entities.Audio
{
    public class Audio : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public AudioClip File { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public bool IsLooping { get; private set; }

        [field: SerializeField]
        public float StartedTime { get; private set; }

        [field: SerializeField]
        public float AmplifyVolumeValue { get; private set; }
    }
}
