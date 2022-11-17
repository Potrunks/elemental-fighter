using System;
using UnityEngine;

namespace Assets.Script.Entities
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Power", menuName = "Power/New Object")]
    public class Power : ScriptableObject
    {
        public string powerName;
        public string description;
        public string type;
        public float speed;
        public float damage;
        public GameObject model;
    }
}
