using UnityEngine;

namespace RuntimeDeveloperToolkit.Core
{
    /// <summary>
    /// Represents the active Runtime Developer Toolkit instance.
    /// 
    /// This class is intentionally kept lightweight. It provides the
    /// lifetime and initialization state of the toolkit and will later
    /// become the host for the toolkit's core services and module system.
    /// </summary>
    public sealed class RuntimeDeveloperToolkitRuntime : MonoBehaviour
    {
        private bool _isInitialized;
        private bool _isShuttingDown;

        /// <summary>
        /// Gets the currently active Runtime Developer Toolkit runtime.
        /// </summary>
        public static RuntimeDeveloperToolkitRuntime Instance { get; private set; }

        /// <summary>
        /// Gets whether the toolkit has completed initialization.
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// Gets whether the toolkit is currently shutting down.
        /// </summary>
        public bool IsShuttingDown => _isShuttingDown;

        /// <summary>
        /// Initializes the toolkit runtime.
        /// </summary>
        internal void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            if (_isShuttingDown)
            {
                return;
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Shuts down the toolkit runtime.
        /// </summary>
        internal void Shutdown()
        {
            if (!_isInitialized)
            {
                return;
            }

            if (_isShuttingDown)
            {
                return;
            }

            _isShuttingDown = true;
            _isInitialized = false;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance != this)
            {
                return;
            }

            Instance = null;
            _isInitialized = false;
            _isShuttingDown = false;
        }
    }
}