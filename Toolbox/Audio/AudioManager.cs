using System.Collections.Generic;

namespace Cawotte.Toolbox.Audio 
{
    
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Act as a Singleton Dictionary so other objects can access to any sound they want to play.
    /// It must be filled in the inspector with all music and sounds.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioManager", menuName = "Audio/AudioManager")]
    public class AudioManager : ScriptableObject
    {

        //We remember at runtime the Sound Player in the audioManager to be able to 
        //perform global operation on them (like mute/resume all)

        [SerializeField] [ReadOnly]
        private List<AudioSourcePlayer> soundSources = new List<AudioSourcePlayer>();

        [SerializeField]
        [Range(0f, 1f)]
        private float volume;

        public float Volume { 
            get => volume; 
            set
            {
                volume = Mathf.Clamp01(value);
                OnVolumeChange?.Invoke(volume); //Trigger events when the volume is changed.
            }
        }

        public MyFloatUnityEvent OnVolumeChange = null;

        #region Public Methods


        public void RegisterAudioSourcePlayer(AudioSourcePlayer player)
        {
            soundSources.Add(player);
            OnVolumeChange.AddListener(player.SetVolumeAllSources);
        }

        public void UnregisterAudioSourcePlayer(AudioSourcePlayer player)
        {
            soundSources.Remove(player);
            OnVolumeChange.RemoveListener(player.SetVolumeAllSources);
        }

        #endregion



    }

    [System.Serializable]
    public class MyFloatUnityEvent : UnityEvent<float>
    {

    }
}