namespace RuntimeDeveloperToolkit.Performance.Data
{
    /// <summary>
    /// Represents a snapshot of runtime performance information.
    /// </summary>
    public readonly struct RuntimePerformanceData
    {
        /// <summary>
        /// Current frames per second.
        /// </summary>
        public float FPS { get; }

        /// <summary>
        /// Current frame time in milliseconds.
        /// </summary>
        public float FrameTimeMilliseconds { get; }

        /// <summary>
        /// Average frames per second over the current measurement period.
        /// </summary>
        public float AverageFPS { get; }

        /// <summary>
        /// Minimum frames per second observed during the current measurement period.
        /// </summary>
        public float MinFPS { get; }

        /// <summary>
        /// Maximum frames per second observed during the current measurement period.
        /// </summary>
        public float MaxFPS { get; }

        /// <summary>
        /// Creates a performance data snapshot.
        /// </summary>
        public RuntimePerformanceData(
            float fps,
            float frameTimeMilliseconds,
            float averageFPS,
            float minFPS,
            float maxFPS)
        {
            FPS = fps;
            FrameTimeMilliseconds = frameTimeMilliseconds;
            AverageFPS = averageFPS;
            MinFPS = minFPS;
            MaxFPS = maxFPS;
        }
    }
}