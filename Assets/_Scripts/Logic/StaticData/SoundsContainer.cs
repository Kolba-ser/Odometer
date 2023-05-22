using System;
using UnityEngine;

namespace Logic.StaticData
{
    [CreateAssetMenu(fileName ="Sounds countainer", menuName ="SO/Sounds/SoundsContainer")]
    public class SoundsContainer : ScriptableObject
    {
        public Sound[] Sounds;


        [Serializable]
        public struct Sound
        {
            public string Name;
            public AudioClip Clip;
        }
    }
}