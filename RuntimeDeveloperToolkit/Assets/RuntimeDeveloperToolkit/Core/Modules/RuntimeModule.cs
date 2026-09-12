namespace RuntimeDeveloperToolkit.Core.Modules
{
    /// <summary>
    /// Base implementation for Runtime Developer Toolkit modules.
    /// </summary>
    public abstract class RuntimeModule : IRuntimeModule
    {
        private bool _isInitialized;
        private bool _isEnabled;

        /// <inheritdoc />
        public abstract string Id { get; }

        /// <inheritdoc />
        public abstract string DisplayName { get; }

        /// <inheritdoc />
        public bool IsEnabled => _isEnabled;

        /// <inheritdoc />
        public bool IsInitialized => _isInitialized;

        /// <inheritdoc />
        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            OnInitialize();

            _isInitialized = true;
        }

        /// <inheritdoc />
        public void Enable()
        {
            if (!_isInitialized || _isEnabled)
            {
                return;
            }

            OnEnable();

            _isEnabled = true;
        }

        /// <inheritdoc />
        public void Disable()
        {
            if (!_isEnabled)
            {
                return;
            }

            OnDisable();

            _isEnabled = false;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isEnabled)
            {
                Disable();
            }

            if (!_isInitialized)
            {
                return;
            }

            OnDispose();

            _isInitialized = false;
        } 

        /// <summary>
        /// Called once when the module is initialized.
        /// </summary>
        protected virtual void OnInitialize()
        {
        }

        /// <summary>
        /// Called when the module is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
        }

        /// <summary>
        /// Called when the module is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Called when the module is disposed.
        /// </summary>
        protected virtual void OnDispose()
        {
        }
    }
}