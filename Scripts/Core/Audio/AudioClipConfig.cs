using UnityEngine;

namespace Ji2Core.Core.Audio
{
    [CreateAssetMenu]
    public class AudioClipConfig : ScriptableObject
    {
        [SerializeField][Range(0.00000001f, 1)] private float playVolume = 1;
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioClipName clipName;

        public AudioClipName ClipName => clipName;
        public float PlayVolume => playVolume;
        public AudioClip Clip => clip;
    }
}