namespace Cawotte.Toolbox.Audio
{
    
    using UnityEngine;

    /// <summary>
    /// Meant to differentiate different kind of sounds, but not used yet in the code.
    /// </summary>
    public enum SoundType
    {
        SFX, Music
    }

    /*
     * Class used to define a sound, any playable sound clips.
     * The audio manager contains an Array of 'Sound' which will all contain a sound.
     * */

    /// <summary>
    /// Class encapsulating a playable sound, 
    /// that must be registered in the AudioManager or a SoundList from the AudioManager.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName ="New Sound", menuName = "Audio/Sound")]
    public class Sound : ScriptableObject
    {

        [Header("Sound Info")]

        [SerializeField]
        private string name; //sound name

        [SerializeField]
        private AudioClip clip; //sound asset

        [SerializeField]
        private SoundType type;

        [Header("Sound parameters")]

        [SerializeField]
        [Range(.1f, 3f)]
        private float pitch = 1f;
        [SerializeField]
        private bool loop = false;

        //component which will play the sound
        [HideInInspector] public AudioSource source;
        
        public float Pitch { get => pitch; }
        public bool Loop { get => loop; set => loop = value; }

        /// <summary>
        /// Load the Sound in the given source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sound"></param>
        public virtual void LoadIn(AudioSource source)
        {
            source.clip = this.clip;
            source.pitch = this.Pitch;
            source.loop = this.Loop;


            source.spatialBlend = 0f; //2D Sounds
        }

    }
}
