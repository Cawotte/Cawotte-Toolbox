namespace Cawotte.Toolbox.Audio
{
    
    using UnityEngine;
    
    /*
     * Class used to define a sound, any playable sound clips.
     * The audio manager contains an Array of 'Sound' which will all contain a sound.
     * */

    /// <summary>
    /// Class encapsulating a playable sound, 
    /// that must be registered in the AudioManager or a SoundList from the AudioManager.
    /// </summary>
    [System.Serializable]
    public class Sound
    {

        public string name; //sound name
        public AudioClip clip; //sound asset

        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 1f;
        [SerializeField]
        [Range(.1f, 3f)]
        private float pitch = 1f;
        [SerializeField]
        private bool loop = false;

        [SerializeField]
        private float minDistance = 0f;
        [SerializeField]
        private float maxDistance = 500f;

        //component which will play the sound
        [HideInInspector] public AudioSource source;
        
        private string listName;

        public float Volume { get => volume; }
        public float Pitch { get => pitch; }
        public float MinDistance { get => minDistance;  }
        public float MaxDistance { get => maxDistance; }

        public bool Loop { get => loop; set => loop = value; }
        public string ListName
        {
            get => listName;
            set => listName = value;
        }


        /// <summary>
        /// Load the Sound in the given source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sound"></param>
        public void LoadIn(AudioSource source)
        {
            source.clip = this.clip;
            source.volume = this.Volume;
            source.pitch = this.Pitch;
            source.loop = this.Loop;
            source.minDistance = this.MinDistance;
            source.maxDistance = this.MaxDistance;
            source.spatialBlend = 1f;
            source.rolloffMode = AudioRolloffMode.Linear;
        }

    }
}
