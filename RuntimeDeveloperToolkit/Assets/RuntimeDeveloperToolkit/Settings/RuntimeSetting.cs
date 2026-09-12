using System;

namespace RuntimeDeveloperToolkit.Settings
{
    internal interface IRuntimeSetting
    {
        string Id { get; }
        void Reset();
    }
    
    public sealed class RuntimeSetting<T> : IRuntimeSetting
    {
        private T _value;

        public string Id { get; }

        public T DefaultValue { get; }

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }

                T previousValue = _value;
                _value = value;

                ValueChanged?.Invoke(previousValue, _value);
            }
        }

        public event Action<T, T> ValueChanged;

        public RuntimeSetting(
            string id,
            T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "Setting ID cannot be null or empty.",
                    nameof(id));
            }

            Id = id;
            DefaultValue = defaultValue;
            _value = defaultValue;
        }

        public void Reset()
        {
            Value = DefaultValue;
        }
    }
} 