using System;

namespace Cawotte.Toolbox.Audio {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "SoundBank", menuName = "ScriptableObjects/Audio/SoundBank")]
    public class SoundBank : ScriptableObject
    {
        
        #region Member and Properties
        
        //Individual Sounds
        [SerializeField]
        private Sound[] sounds;

        //List of similar sounds that can be swapped.
        [SerializeField]
        private SoundList[] soundLists;
        
        [SerializeField]
        private Sound[] musics;

        public Sound[] Sounds { get => sounds; }
        public Sound[] Musics { get => musics; }
        public SoundList[] SoundLists { get => soundLists; }
        
        #endregion
        
        #region Public Methods

        public void Awake()
        {
            //Initialize sounds lists
            foreach (SoundList list in soundLists)
            {
                soundLists.Initialize();
            }
            //Initialize sounds lists
            foreach (Sound music in musics)
            {
                Debug.Log("Music : " + music.name);
            }
            //Initialize sounds lists
            foreach (Sound sound in sounds)
            {
                Debug.Log("Sounds : " + sound.name);
            }
        }

        public Sound FindSound(string soundName)
        {
            return Find(sounds, soundName);
        }
        
        public Sound FindMusic(string soundName)
        {
            return Find(musics, soundName);
        }

        /// <summary>
        /// Return the SoundList with the given soundName. 
        /// Used by other objets to access the sounds they want to play.        
        /// </summary>
        /// <param soundName="listName"></param>
        /// <returns></returns>
        public SoundList FindList(string listName)
        {
            SoundList s = Array.Find(soundLists, list => list.Name == listName);
            if (s == null)
            {
                Debug.LogWarning("SoundList:" + listName + " not found!");
            }
            return s;
        }
        
        /// <summary>
        /// Return the Sound object with the given soundName. 
        /// Used by other objects to access the sounds they want to play.
        /// </summary>
        /// <param soundName="soundName"></param>
        /// <returns></returns>
        private Sound Find(Sound[] soundArray, string soundName)
        {
            Sound s = Array.Find(soundArray, sound => sound.name == soundName);
            if (s == null)
            {
                foreach (SoundList list in soundLists)
                {
                    s = list.Find(soundName);
                    if (s != null) break;
                }
                
                if (s == null)
                {
                    Debug.LogWarning("Sound:" + soundName + " not found!");
                }
            }
            return s;
        }
        
        #endregion
    }
}
