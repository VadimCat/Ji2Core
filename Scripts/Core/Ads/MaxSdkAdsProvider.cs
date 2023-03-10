using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ji2Core.Ads
{
    public class MaxSdkAdsProvider : IAdsProvider
    {
        private const string key =
            "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";

        private const string InterstitialAdUnit = "d63d645128226c3d";

        public async UniTask InitializeAsync()
        {
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxSdkInitialized;
            MaxSdk.SetSdkKey(key);
            MaxSdk.InitializeSdk();

            MaxSdk.LoadInterstitial(InterstitialAdUnit);
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += HandleInterstitialLoadFail;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += HadleInterHide;
            await taskCompletionSource.Task;

            void OnMaxSdkInitialized(MaxSdkBase.SdkConfiguration obj)
            {
                taskCompletionSource.TrySetResult(obj.IsSuccessfullyInitialized);
            }
        }

        private void HadleInterHide(string adUnit, MaxSdkBase.AdInfo info)
        {
            MaxSdk.LoadInterstitial(InterstitialAdUnit);
        }

        public async UniTask<bool> InterstitialAsync(CancellationToken cancellationToken)
        {
            UniTaskCompletionSource<bool> completionSource = new UniTaskCompletionSource<bool>();

            await LoadInterAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            if (MaxSdk.IsInterstitialReady(InterstitialAdUnit))
            {
                MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += HandleAdHide;
                MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += HandleAdFail;
                MaxSdk.ShowInterstitial(InterstitialAdUnit);
                return await completionSource.Task;
            }

            return false;

            void HandleAdHide(string adUnit, MaxSdkBase.AdInfo info)
            {
                completionSource.TrySetResult(true);
            }

            void HandleAdFail(string adUnit, MaxSdkBase.ErrorInfo info)
            {
                completionSource.TrySetResult(false);
            }
        }

        private async UniTask LoadInterAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => MaxSdk.IsInterstitialReady(InterstitialAdUnit),
                cancellationToken: cancellationToken);
        }

        private async void HandleInterstitialLoadFail(string arg1, MaxSdkBase.ErrorInfo arg2)
        {
            if (!MaxSdk.IsInterstitialReady(InterstitialAdUnit))
            {
                await UniTask.Delay(1000);
                MaxSdk.LoadInterstitial(InterstitialAdUnit);
            }
        }
    }
}