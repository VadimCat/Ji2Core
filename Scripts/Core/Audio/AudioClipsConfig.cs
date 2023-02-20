using System.Collections.Generic;
using UnityEngine;

namespace Ji2Core.Core.Audio
{
    [CreateAssetMenu]
    public class AudioClipsConfig : ScriptableObject, IBootstrapable
    {
        [SerializeField] private AudioClipConfig[] clips;
        
        private Dictionary<string, AudioClipConfig> clipsDict = new();
        
        public void Bootstrap()
        {
            foreach (var clip in clips)
            {
                clipsDict[clip.ClipName] = clip;
            }
        }
        
        public AudioClipConfig GetClip(string clipName)
        {
            return clipsDict[clipName];
        }
    }
}