using Cysharp.Threading.Tasks;
using Ji2.Utils;
using Ji2Core.Core.Pools;
using UnityEngine;
using UnityEngine.Audio;

namespace Ji2.Audio
{
    public class Sound : MonoBehaviour
    {
        private const string SfxKey = "SfxVolume";
        private const string MusicKey = "MusicVolume";

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private AudioClipsConfig clipsConfig;
        [SerializeField] private AudioMixer mixer;
        
        [SerializeField] private SfxPlaybackSource _playbackSource;

        public ReactiveProperty<float> SfxVolume;
        public ReactiveProperty<float> MusicVolume;

        private AudioSettings _audioSettings;
        private Pool<SfxPlaybackSource> _sfxPlaybackPool; 

        public void Bootstrap()
        {
            _sfxPlaybackPool = new Pool<SfxPlaybackSource>(_playbackSource, transform);
            _audioSettings = new();
            clipsConfig.Bootstrap();
            SfxVolume = new ReactiveProperty<float>((_audioSettings.SfxLevel * audioConfig.MaxSfxLevel).ToAudioLevel());
            MusicVolume = new ReactiveProperty<float>((_audioSettings.MusicLevel * audioConfig.MaxMusicLevel).ToAudioLevel());
            
            mixer.SetFloat(SfxKey, SfxVolume.Value);
            mixer.SetFloat(MusicKey, MusicVolume.Value);
        }

        public void SetSfxLevel(float level)
        {
            var groupVolume = (audioConfig.MaxSfxLevel * level).ToAudioLevel();
            sfxSource.outputAudioMixerGroup.audioMixer.SetFloat(MusicKey, groupVolume);

            _audioSettings.SfxLevel = level;
            SfxVolume.Value = level;
            
            _audioSettings.Save();
        }

        public void SetMusicLevel(float level)
        {
            var groupVolume = (audioConfig.MaxMusicLevel * level).ToAudioLevel();
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat(SfxKey, groupVolume);

            _audioSettings.MusicLevel = level;
            MusicVolume.Value = level;
            
            _audioSettings.Save();
        }

        public void PlayMusic(string clipName)
        {
            var clipConfig = clipsConfig.GetClip(clipName);
            musicSource.clip = clipConfig.Clip;
            musicSource.Play();
            musicSource.clip.LoadAudioData();
        }
        
        public async UniTask PlaySfxAsync(string clipName)
        {
            var source = _sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            await source.PlaybackAsync();
            _sfxPlaybackPool.DeSpawn(source);
        }

        public SfxPlaybackSource GetPlaybackSource(string clipName)
        {
            var source = _sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            return source;
        }

        public void ReleasePlaybackSource(SfxPlaybackSource playbackSource)
        {
            _sfxPlaybackPool.DeSpawn(playbackSource);
        }
    }
}