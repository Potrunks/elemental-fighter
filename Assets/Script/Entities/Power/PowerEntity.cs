using Assets.Script.Data;
using System;
using UnityEngine;

namespace Assets.Script.Entities
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Power", menuName = "Power/New")]
    public class PowerEntity : ScriptableObject
    {
        public string powerName;
        public string powerDescription;
        public PowerTypeReference powerType;
        public PowerLevelReference powerLevel;
        public float powerSpeed;
        public int powerDamage;
        public GameObject powerModel;
        public Sound powerSound;
    }
}
