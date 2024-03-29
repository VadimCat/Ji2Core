using Cysharp.Threading.Tasks;
using Ji2.Pools;
using UnityEngine;

namespace Ji2.Audio
{
    public class SfxPlaybackSource : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioSource source;
        private UniTaskCompletionSource<bool> _completionSource;

        public bool IsPlaying => source is { isPlaying: true };
        
        public void SetDependencies(AudioClipConfig clipConfig)
        {
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