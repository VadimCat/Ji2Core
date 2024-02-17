using UnityEngine;

namespace Ji2.Audio
{
    [CreateAssetMenu]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField][Range(0.0000001f, 1)] private float maxSfxLevel;
        [SerializeField][Range(0.0000001f, 1)] private float maxMusicLevel;
        
        public float MaxSfxLevel => maxSfxLevel;
        public float MaxMusicLevel => maxMusicLevel;
    }
}