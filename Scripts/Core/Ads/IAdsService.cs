using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ji2Core.Ads
{
    public interface IAdsProvider
    {
        public UniTask InitializeAsync();
        public UniTask<bool> InterstitialAsync(CancellationToken cancellationToken);
    }
}