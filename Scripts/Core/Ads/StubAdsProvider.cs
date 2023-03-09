using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ji2Core.Ads
{
    public class StubAdsProvider : IAdsProvider
    {
        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public UniTask<bool> InterstitialAsync(CancellationToken cancellationToken)
        {
            return UniTask.FromResult(true);
        }
    }
}