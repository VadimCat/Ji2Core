using Cysharp.Threading.Tasks;
using Ji2Core.Core.Pools;
using UnityEngine;

namespace Ji2Core.Core.Audio
{
    public class SfxPlaybackSource : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioSource source;
        private AudioClipConfig clipConfig;
        private UniTaskCompletionSource<bool> completionSource;

        public bool IsPlaying => source.isPlaying;
        
        public void SetDependencies(AudioClipConfig clipConfig)
        {
            this.clipConfig = clipConfig;
            source.clip = clipConfig.Clip;
            source.volume = clipConfig.PlayVolume;
        }
    
        public async UniTask PlaybackAsync(bool isLooped = false)
        {
            source.loop = isLooped;
            source.Play();
            await UniTask.WaitWhile(() => IsPlaying);
        }

        public void Pause()
        {
            source.Pause();
        }
        
        public void Stop()
        {
            source.Stop();
        }

        public void Spawn()
        {
            gameObject.SetActive(true);
        }

        public void DeSpawn()
        {
            gameObject.SetActive(false);

            source.loop = false;
            source.Stop();
            source.clip = null;
        }
    }
}