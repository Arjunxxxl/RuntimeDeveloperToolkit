using UnityEngine;
using RuntimeDeveloperToolkit.Performance.Data;

namespace RuntimeDeveloperToolkit.Performance.Services
{
    /// <summary>
    /// Collects and calculates runtime frame performance statistics.
    /// </summary>
    public sealed class RuntimePerformanceSampler
    {
        private float _currentFPS;
        private float _currentFrameTimeMilliseconds;

        private float _averageFPS;
        private float _minFPS;
        private float _maxFPS;

        private float _fpsSum;
        private int _sampleCount;

        /// <summary>
        /// Gets the most recently calculated FPS.
        /// </summary>
        public float FPS => _currentFPS;

        /// <summary>
        /// Gets the most recent frame time in milliseconds.
        /// </summary>
        public float FrameTimeMilliseconds =>
            _currentFrameTimeMilliseconds;

        /// <summary>
        /// Gets the average FPS since the last reset.
        /// </summary>
        public float AverageFPS => _averageFPS;

        /// <summary>
        /// Gets the minimum FPS since the last reset.
        /// </summary>
        public float MinFPS => _minFPS;

        /// <summary>
        /// Gets the maximum FPS since the last reset.
        /// </summary>
        public float MaxFPS => _maxFPS;
        
        private RuntimePerformanceHistory _history;
        
        /// <summary>
        /// Gets the performance history buffer.
        /// </summary>
        public RuntimePerformanceHistory History => _history;
        
        /// <summary>
        /// Creates a performance sampler.
        /// </summary>
        /// <param name="historyCapacity">
        /// Maximum number of performance samples to retain.
        /// </param>
        public RuntimePerformanceSampler(int historyCapacity = 120)
        {
            _history = new RuntimePerformanceHistory(historyCapacity);
        }

        /// <summary>
        /// Samples the current frame.
        /// </summary>
        public void Sample(float deltaTime)
        {
            if (deltaTime <= 0f)
            {
                return;
            }

            _currentFrameTimeMilliseconds =
                deltaTime * 1000f;

            _currentFPS =
                1f / deltaTime;

            _fpsSum += _currentFPS;
            _sampleCount++;

            _averageFPS =
                _fpsSum / _sampleCount;

            if (_sampleCount == 1)
            {
                _minFPS = _currentFPS;
                _maxFPS = _currentFPS;
            }
            else
            {
                if (_currentFPS < _minFPS)
                {
                    _minFPS = _currentFPS;
                }

                if (_currentFPS > _maxFPS)
                {
                    _maxFPS = _currentFPS;
                }
            }
            
            _history.Add(
                _currentFPS,
                _currentFrameTimeMilliseconds);
        }

        /// <summary>
        /// Resets all collected statistics.
        /// </summary>
        public void Reset()
        {
            _currentFPS = 0f;
            _currentFrameTimeMilliseconds = 0f;

            _averageFPS = 0f;
            _minFPS = 0f;
            _maxFPS = 0f;

            _fpsSum = 0f;
            _sampleCount = 0;
        }
    }
}