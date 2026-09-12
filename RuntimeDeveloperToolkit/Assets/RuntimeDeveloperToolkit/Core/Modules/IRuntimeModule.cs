namespace RuntimeDeveloperToolkit.Core.Modules
{
    /// <summary>
    /// Defines the lifecycle contract for a Runtime Developer Toolkit module.
    /// </summary>
    public interface IRuntimeModule
    {
        /// <summary>
        /// Unique identifier for this module.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Display name used by developer-facing tools.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Determines whether this module is currently enabled.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Determines whether this module has been initialized.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Enables the module.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the module.
        /// </summary>
        void Disable();

        /// <summary>
        /// Releases resources owned by the module.
        /// </summary>
        void Dispose(); 
    }
}