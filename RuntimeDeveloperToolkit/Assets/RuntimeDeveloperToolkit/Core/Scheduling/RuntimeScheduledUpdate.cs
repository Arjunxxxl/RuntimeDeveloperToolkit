using System;

namespace RuntimeDeveloperToolkit.Core.Scheduling
{
    internal sealed class RuntimeScheduledUpdate
    {
        public Action<RuntimeUpdateContext> Callback { get; }
        public RuntimeUpdateRate Rate { get; }

        public float Interval { get; }

        public float Accumulator { get; set; }

        public bool IsActive { get; set; }

        public RuntimeScheduledUpdate(
            Action<RuntimeUpdateContext> callback,
            RuntimeUpdateRate rate)
        {
            Callback = callback;
            Rate = rate;
            Interval = GetInterval(rate);
            Accumulator = 0f;
            IsActive = true;
        }

        private static float GetInterval(RuntimeUpdateRate rate)
        {
            switch (rate)
            {
                case RuntimeUpdateRate.EveryFrame:
                    return 0f;

                case RuntimeUpdateRate.Hz30:
                    return 1f / 30f;

                case RuntimeUpdateRate.Hz10:
                    return 1f / 10f;

                case RuntimeUpdateRate.Hz5:
                    return 1f / 5f;

                case RuntimeUpdateRate.Hz1:
                    return 1f;

                case RuntimeUpdateRate.Manual:
                    return 0f;

                default:
                    throw new ArgumentOutOfRangeException(nameof(rate), rate, null);
            }
        }
    }
} 