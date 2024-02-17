using System.Collections.Generic;
using UnityEngine;

namespace Ji2.Audio
{
    [CreateAssetMenu]
    public class AudioClipsConfig : ScriptableObject
    {
        [SerializeField] private AudioClipConfig[] clips;
        
        private Dictionary<string, AudioClipConfig> _clipsDict = new();
        
        public void Bootstrap()
        {
            foreach (var clip in clips)
            {
                _clipsDict[clip.ClipName] = clip;
            }
        }
        
        public AudioClipConfig GetClip(string clipName)
        {
            return _clipsDict[clipName];
        }
    }
}