using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ji2.UI
{
    public class CompositeProgressBar : SerializedMonoBehaviour, IProgressBar
    {
        [ShowInInspector, SerializeField] private IProgressBar[] bars;


        public UniTask AnimateProgressAsync(float normalProgress)
        {
            var tasks = new List<UniTask>(bars.Length);
            foreach (var bar in bars)
            {
                tasks.Add(bar.AnimateProgressAsync(normalProgress));
            }

            return UniTask.WhenAll(tasks);
        }

        public UniTask AnimateFakeProgressAsync(float duration)
        {
            var tasks = new List<UniTask>(bars.Length);
            foreach (var bar in bars)
            {
                tasks.Add(bar.AnimateFakeProgressAsync(duration));
            }

            return UniTask.WhenAll(tasks);
        }
    }
}