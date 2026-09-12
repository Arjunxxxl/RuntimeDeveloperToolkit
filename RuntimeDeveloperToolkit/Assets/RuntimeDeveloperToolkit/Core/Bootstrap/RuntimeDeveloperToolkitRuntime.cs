using UnityEngine;
using RuntimeDeveloperToolkit.Core.Modules;
using RuntimeDeveloperToolkit.Core.Scheduling;

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
        /// Gets the module registry.
        /// </summary>
        public RuntimeModuleRegistry Modules { get; private set; }
        
        /// <summary>
        /// Get the Update Scheduler
        /// </summary>
        public RuntimeUpdateScheduler Scheduler { get; private set; }
        
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

            Modules = new RuntimeModuleRegistry();
            Scheduler = new RuntimeUpdateScheduler();
            
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

            Modules?.DisableAll();
            Modules?.DisposeAll();
            Modules = null;
            
            Scheduler?.Clear();
            
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
        
        private void Update()
        {
            if (!_isInitialized || _isShuttingDown)
            {
                return;
            }

            Scheduler.Update(
                Time.deltaTime,
                Time.unscaledDeltaTime,
                Time.time,
                Time.unscaledTime);
        }

        private void OnDestroy()
        {
            if (Instance != this)
            {
                return;
            }

            Shutdown();
            
            Instance = null;
            _isInitialized = false;
            _isShuttingDown = false;
        }
    }
}