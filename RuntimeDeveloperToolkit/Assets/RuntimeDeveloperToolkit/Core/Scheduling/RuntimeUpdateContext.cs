namespace RuntimeDeveloperToolkit.Core.Scheduling
{
    public readonly struct RuntimeUpdateContext
    {
        public float DeltaTime { get; }
        public float UnscaledDeltaTime { get; }
        public float ElapsedTime { get; }
        public float UnscaledElapsedTime { get; }

        public RuntimeUpdateContext(
            float deltaTime,
            float unscaledDeltaTime,
            float elapsedTime,
            float unscaledElapsedTime)
        {
            DeltaTime = deltaTime;
            UnscaledDeltaTime = unscaledDeltaTime;
            ElapsedTime = elapsedTime;
            UnscaledElapsedTime = unscaledElapsedTime;
        }
    }
} 