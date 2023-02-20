using Cysharp.Threading.Tasks;
using Ji2.Utils;
using Ji2Core.Core.Pools;
using UnityEngine;
using UnityEngine.Audio;

namespace Ji2Core.Core.Audio
{
    public class AudioService : MonoBehaviour, IBootstrapable
    {
        private const string SFX_KEY = "SfxVolume";
        private const string MUSIC_KEY = "MusicVolume";

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private AudioClipsConfig clipsConfig;
        [SerializeField] private AudioMixer mixer;
        
        [SerializeField] private SfxPlaybackSource playbackSource;

        public ReactiveProperty<float> sfxVolume;
        public ReactiveProperty<float> musicVolume;

        private AudioSettings audioSettings;
        private Pool<SfxPlaybackSource> sfxPlaybackPool; 

        public void Bootstrap()
        {
            sfxPlaybackPool = new Pool<SfxPlaybackSource>(playbackSource, transform);
            audioSettings = new();
            clipsConfig.Bootstrap();
            sfxVolume = new ReactiveProperty<float>((audioSettings.SfxLevel * audioConfig.MaxSfxLevel).ToAudioLevel());
            musicVolume = new ReactiveProperty<float>((audioSettings.MusicLevel * audioConfig.MaxMusicLevel).ToAudioLevel());
            
            mixer.SetFloat(SFX_KEY, sfxVolume.Value);
            mixer.SetFloat(MUSIC_KEY, musicVolume.Value);
        }

        public void SetSfxLevel(float level)
        {
            var groupVolume = (audioConfig.MaxSfxLevel * level).ToAudioLevel();
            sfxSource.outputAudioMixerGroup.audioMixer.SetFloat(MUSIC_KEY, groupVolume);

            audioSettings.SfxLevel = level;
            sfxVolume.Value = level;
            
            audioSettings.Save();
        }

        public void SetMusicLevel(float level)
        {
            var groupVolume = (audioConfig.MaxMusicLevel * level).ToAudioLevel();
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat(SFX_KEY, groupVolume);

            audioSettings.MusicLevel = level;
            musicVolume.Value = level;
            
            audioSettings.Save();
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
            var source = sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            await source.PlaybackAsync();
            sfxPlaybackPool.DeSpawn(source);
        }

        public SfxPlaybackSource GetPlaybackSource(string clipName)
        {
            var source = sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            return source;
        }

        public void ReleasePlaybackSource(SfxPlaybackSource playbackSource)
        {
            sfxPlaybackPool.DeSpawn(playbackSource);
        }
    }
}