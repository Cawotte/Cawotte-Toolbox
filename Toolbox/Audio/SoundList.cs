namespace Cawotte.Toolbox.Audio
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Encapsulate a list of sounds, used most often when sounds are swappable and can be used in the same scenario,
    /// so we fetch a random one from the list.
    /// </summary>
    [System.Serializable]
    public class SoundList
    {
        [SerializeField]
        private string listName;
        [SerializeField]
        private Sound[] sounds;

        public Sound[] Sounds { get => sounds; }
        public string Name { get => listName; }
        
        /// <summary>
        /// Initialize all sounds with this list's soundName, to which they belong.
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].ListName = listName;
            }
        }

        /// <summary>
        /// Find a sound with the given soundName in the list. 
        /// Return null if not found. 
        /// </summary>
        /// <param soundName="soundName"></param>
        /// <returns></returns>
        public Sound Find(string soundName)
        {
            Sound s = Array.Find(Sounds, sound => sound.name == soundName);
            return s;
        }

        /// <summary>
        /// Get a random sound from the List
        /// </summary>
        /// <returns></returns>
        public Sound GetRandom()
        {
            return sounds[UnityEngine.Random.Range(0, sounds.Length)];
        }

    }
}