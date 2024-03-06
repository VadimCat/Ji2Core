using System;

namespace Ji2.Utils
{
    public class ReactiveProperty<T> : IDisposable
    {
        private T _value;
        private T _prevValue;

        public event Action<T, T> EventValueChangedWithPrevious;
        public event Action<T> EventValueChanged;

        public T PrevValue => _prevValue;

        public T Value
        {
            get => _value;
            set
            {
                _prevValue = _value;
                if (!_value.Equals(value))
                {
                    _value = value;
                    EventValueChangedWithPrevious?.Invoke(_value, _prevValue);
                    EventValueChanged?.Invoke(_value);
                }
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
            EventValueChangedWithPrevious = null;
        }
    }
}