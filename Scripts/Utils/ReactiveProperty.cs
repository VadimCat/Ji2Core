using System;

namespace Utils
{
    public class ReactiveProperty<T> : IDisposable
    {
        private T _value;

        public event Action<T, T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                OnValueChanged?.Invoke(value, _value);
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