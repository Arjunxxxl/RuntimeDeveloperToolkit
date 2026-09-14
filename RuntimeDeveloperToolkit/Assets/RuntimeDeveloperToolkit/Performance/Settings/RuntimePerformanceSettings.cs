namespace RuntimeDeveloperToolkit.Performance.Settings
{
    /// <summary>
    /// Configuration for runtime performance monitoring.
    /// </summary>
    public sealed class RuntimePerformanceSettings
    {
        /// <summary>
        /// Gets or sets whether performance monitoring is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets how frequently performance samples are collected.
        /// A value of zero means every frame.
        /// </summary>
        public float SamplingInterval { get; set; } = 0f;

        /// <summary>
        /// Gets or sets the number of performance samples retained
        /// in the history buffer.
        /// </summary>
        public int HistoryCapacity { get; set; } = 120;
    }
}