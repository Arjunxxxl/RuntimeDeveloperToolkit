namespace RuntimeDeveloperToolkit.Performance.Data
{
    /// <summary>
    /// Stores a fixed-size history of performance samples.
    /// </summary>
    public sealed class RuntimePerformanceHistory
    {
        private readonly float[] _fpsSamples;
        private readonly float[] _frameTimeSamples;

        private int _writeIndex;
        private int _count;

        /// <summary>
        /// Gets the maximum number of samples stored.
        /// </summary>
        public int Capacity => _fpsSamples.Length;

        /// <summary>
        /// Gets the number of samples currently stored.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Creates a performance history buffer.
        /// </summary>
        /// <param name="capacity">
        /// Maximum number of samples to retain.
        /// </param>
        public RuntimePerformanceHistory(int capacity)
        {
            if (capacity <= 0)
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(capacity),
                    "Capacity must be greater than zero.");
            }

            _fpsSamples = new float[capacity];
            _frameTimeSamples = new float[capacity];
        }

        /// <summary>
        /// Adds a performance sample.
        /// </summary>
        public void Add(float fps, float frameTimeMilliseconds)
        {
            _fpsSamples[_writeIndex] = fps;
            _frameTimeSamples[_writeIndex] = frameTimeMilliseconds;

            _writeIndex++;
            
            if (_writeIndex >= Capacity)
            {
                _writeIndex = 0;
            }

            if (_count < Capacity)
            {
                _count++;
            }
        }

        /// <summary>
        /// Gets an FPS sample by chronological index.
        /// Index zero represents the oldest available sample.
        /// </summary>
        public float GetFPS(int index)
        {
            ValidateIndex(index);

            int bufferIndex =
                GetBufferIndex(index);

            return _fpsSamples[bufferIndex];
        }

        /// <summary>
        /// Gets a frame-time sample by chronological index.
        /// Index zero represents the oldest available sample.
        /// </summary>
        public float GetFrameTimeMilliseconds(int index)
        {
            ValidateIndex(index);

            int bufferIndex =
                GetBufferIndex(index);

            return _frameTimeSamples[bufferIndex];
        }

        /// <summary>
        /// Clears all stored samples.
        /// </summary>
        public void Clear()
        {
            _writeIndex = 0;
            _count = 0;
        }

        private int GetBufferIndex(int chronologicalIndex)
        {
            int oldestIndex =
                _count == Capacity
                    ? _writeIndex
                    : 0;

            return (oldestIndex + chronologicalIndex) % Capacity;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(index));
            }
        }
    }
}