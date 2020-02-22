using System.Collections.Generic;

namespace Cawotte.Toolbox.Audio 
{
    
    using System;
    using UnityEngine;

    /// <summary>
    /// Act as a Singleton Dictionary so other objects can access to any sound they want to play.
    /// It must be filled in the inspector with all music and sounds.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {

        [SerializeField] private SoundBank soundBank;


        
        private AudioSourcePlayer globalPlayer; //Used for musics
        
        [SerializeField]
        private String[] musicsToPlay;


        protected override void OnAwake()
        {
            globalPlayer = gameObject.AddComponent<AudioSourcePlayer>();
            soundBank.Awake();
        }

        void Start()
        {

            globalPlayer.PlayMusic(musicsToPlay[0]);
            
            Sound music = FindMusic(musicsToPlay[1]);
            if (music != null)
            {
                Debug.Log(music.name);
                globalPlayer.Play(music);
            }

        }

        #region Public Methods
        

        /// <summary>
        /// Return the Sound object with the given name. 
        /// Used by other objets to access the sounds they want to play.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Sound FindSound(string name)
        {
            return soundBank.FindSound(name);
        }
        
        /// <summary>
        /// Return the music with the given name. 
        /// Used by other objets to access the musics they want to play.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Sound FindMusic(string name)
        {
            return soundBank.FindMusic(name);
        }

        /// <summary>
        /// Return the SoundList with the given name. 
        /// Used by other objets to access the sounds they want to play.        
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SoundList FindList(string name)
        {
            return soundBank.FindList(name);
        }

        #endregion


    }
}