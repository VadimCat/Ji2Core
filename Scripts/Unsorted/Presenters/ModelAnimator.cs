using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ji2.Presenters
{
    public class ModelAnimator
    {
        private List<UniTask> _animations = new();
        private Queue<Func<UniTask>> _animationsQueue = new();
            
        public async UniTask Animate(UniTask uniTask)
        {
            _animations.Add(uniTask);
            await uniTask;
            if (_animationsQueue.TryDequeue(out var result))
            {
                await Animate(result());
                _animations.Remove(uniTask);
            }
            else
            {
                _animations.Remove(uniTask);
            }
        }

        public async UniTask Enqueue(Action action)
        {
            if (CheckAnimationsListEmpty())
            {
                action();
            }
            else
            {
                _animationsQueue.Enqueue(() =>
                {
                    action();
                    return UniTask.CompletedTask;
                });
            }
        }
        
        public async UniTask Enqueue(Func<UniTask> animationFunc)
        {
            if (CheckAnimationsListEmpty())
            {
                await Animate(animationFunc());
            }
            else
            {
                _animationsQueue.Enqueue(animationFunc);
            }
        }

        public async UniTask AwaitAllAnimationsEnd(CancellationToken cancellationToken = default)
        {
            await UniTask.WaitUntil(CheckAnimationsListEmpty, cancellationToken: cancellationToken);
        }
        
        private bool CheckAnimationsListEmpty()
        {
            return _animations.Count == 0 && _animationsQueue.Count == 0;
        }
    }
}