using System;

namespace Ji2.Utils
{
    public class ReactiveProperty<T> : IDisposable
    {
        private T _value;
        private T _prevValue;

        public event Action<T, T> OnValueChanged;

        public T PrevValue => _prevValue;

        public T Value
        {
            get => _value;
            set
            {
                OnValueChanged?.Invoke(value, _value);
                _prevValue = _value;
                _value = value;
            }
        }

        public ReactiveProperty(T initialValue)
        {
            Value = initialValue;
        }

        public ReactiveProperty()
        {
            Value = default;
        }

        public void Dispose()
        {
            OnValueChanged = null;
        }
    }
}