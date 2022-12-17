using System;

namespace Assets.Scripts.Utilities
{
    public class ObservebleData<T>
    {
        public event Action<T> OnValueChange;
        private T _value;
        public ObservebleData(T value) {
            Value = value;
        }
        
        public T Value
        {
            get { return _value; }
            set {
                _value = value;
                OnValueChange?.Invoke(value);
            }
        }
    }
}
