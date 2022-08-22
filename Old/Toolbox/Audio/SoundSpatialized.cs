namespace Cawotte.Toolbox.Audio
{
    using UnityEngine;

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Spatialized Sound", menuName = "Audio/Spatialized Sound")]
    public class SoundSpatialized : Sound
    {

        //No volume spatialisation
        [SerializeField]
        private float minDistance = 0f;
        [SerializeField]
        private float maxDistance = 500f;

        public float MinDistance { get => minDistance; }
        public float MaxDistance { get => maxDistance; }

        /// <summary>
        /// Load the Sound in the given source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sound"></param>
        public override void LoadIn(AudioSource source)
        {
            base.LoadIn(source);

            //Spatialization
            source.minDistance = this.MinDistance;
            source.maxDistance = this.MaxDistance;
            source.spatialBlend = 1f;
            source.rolloffMode = AudioRolloffMode.Linear;
        }
    }
}
