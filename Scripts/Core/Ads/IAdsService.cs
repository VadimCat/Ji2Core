using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ji2Core.Ads
{
    public interface IAdsProvider
    {
        public UniTask InitializeAsync();
        public UniTask<bool> InterstitialAsync();
    }

    public class StubAdsProvider : IAdsProvider
    {
        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public UniTask<bool> InterstitialAsync()
        {
            return UniTask.FromResult(true);
        }
    }

    public class MaxSdkAdsProvider : IAdsProvider
    {
        private const string key =
            "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";

        private const string InterstitialAdUnit = "null";

        public async UniTask InitializeAsync()
        {
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxSdkInitialized;
            MaxSdk.SetSdkKey(key);
            MaxSdk.InitializeSdk();
            
            await taskCompletionSource.Task;

            void OnMaxSdkInitialized(MaxSdkBase.SdkConfiguration obj)
            {
                Debug.LogError(obj.IsSuccessfullyInitialized);
                taskCompletionSource.TrySetResult(obj.IsSuccessfullyInitialized);
            }
        }

        private void OnInterLoaded(string adUnit, MaxSdkBase.AdInfo info)
        {
        }

        private UniTask<bool> LoadInter()
        {
            if (MaxSdk.IsInterstitialReady(InterstitialAdUnit))
            {
            }
            else
            {
            }

            return UniTask.FromResult(true);
        }

        public UniTask<bool> InterstitialAsync()
        {
            // MaxSdk.ShowInterstitial(InterstitialAdUnit);
            // UniTaskCompletionSource<bool> taskCompletionSource = new UniTaskCompletionSource<bool>();
            // MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += AdDisplayed;
            // MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += Ad
            //     
            // await taskCompletionSource.Task;
            //
            // void AdDisplayed(string arg1, MaxSdkBase.AdInfo arg2)
            // {
            //     taskCompletionSource.TrySetResult(true);
            // }
            return UniTask.FromResult(true);
        }
    }
}