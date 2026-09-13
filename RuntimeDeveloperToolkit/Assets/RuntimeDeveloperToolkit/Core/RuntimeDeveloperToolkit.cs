using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Core.Modules;
using RuntimeDeveloperToolkit.Core.Scheduling;
using RuntimeDeveloperToolkit.Core.Services;

namespace RuntimeDeveloperToolkit
{
    /// <summary>
    /// Public entry point for the Runtime Developer Toolkit.
    /// </summary>
    public static class RuntimeDeveloperToolkit
    {
        /// <summary>
        /// Gets whether the toolkit runtime has been initialized.
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                RuntimeDeveloperToolkitRuntime runtime =
                    RuntimeDeveloperToolkitRuntime.Instance;

                return runtime != null &&
                       runtime.IsInitialized;
            }
        }

        /// <summary>
        /// Gets whether the toolkit runtime is shutting down.
        /// </summary>
        public static bool IsShuttingDown
        {
            get
            {
                RuntimeDeveloperToolkitRuntime runtime =
                    RuntimeDeveloperToolkitRuntime.Instance;

                return runtime != null &&
                       runtime.IsShuttingDown;
            }
        }

        /// <summary>
        /// Gets the runtime module registry.
        /// </summary>
        public static RuntimeModuleRegistry Modules
        {
            get
            {
                RuntimeDeveloperToolkitRuntime runtime =
                    RuntimeDeveloperToolkitRuntime.Instance;

                return runtime != null
                    ? runtime.Modules
                    : null;
            }
        }

        /// <summary>
        /// Gets the runtime update scheduler.
        /// </summary>
        public static RuntimeUpdateScheduler Scheduler
        {
            get
            {
                RuntimeDeveloperToolkitRuntime runtime =
                    RuntimeDeveloperToolkitRuntime.Instance;

                return runtime != null
                    ? runtime.Scheduler
                    : null;
            }
        }
        
        /// <summary>
        /// Gets the runtime service registry.
        /// </summary>
        public static RuntimeServiceRegistry Services
        {
            get
            {
                RuntimeDeveloperToolkitRuntime runtime =
                    RuntimeDeveloperToolkitRuntime.Instance;

                return runtime != null
                    ? runtime.Services
                    : null;
            }
        } 
    }
}