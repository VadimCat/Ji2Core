using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ji2Core.Core.Audio
{
    [CreateAssetMenu]
    public class AudioClipConfig : SerializedScriptableObject
    {
        [SerializeField] private ISoundNamesCollection _soundNamesOrigin;
        
        [SerializeField][Range(0.00000001f, 1)] private float playVolume = 1;
        [SerializeField] private AudioClip clip;
        [SerializeField, ValueDropdown("_dropdownItems")] private string clipName;

        private IEnumerable<string> _dropdownItems => _soundNamesOrigin.SoundsList;
        
        public string ClipName => clipName;
        public float PlayVolume => playVolume;
        public AudioClip Clip => clip;
    }
}