using RuntimeDeveloperToolkit.Core.Services;

namespace RuntimeDeveloperToolkit.Settings
{
    public sealed class RuntimeSettingsService : IRuntimeService
    {
        private RuntimeSettingsRegistry _registry;

        public string Id => "settings";

        public bool IsInitialized =>
            _registry != null;

        public RuntimeSettingsRegistry Registry =>
            _registry;

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            _registry = new RuntimeSettingsRegistry();
        }

        public void Shutdown()
        {
            if (!IsInitialized)
            {
                return;
            }

            _registry.Clear();
            _registry = null;
        }

        public bool RegisterGroup(
            string groupId,
            RuntimeSettings settings)
        {
            if (!IsInitialized)
            {
                return false;
            }

            return _registry.RegisterGroup(
                groupId,
                settings);
        }

        public bool UnregisterGroup(
            string groupId)
        {
            if (!IsInitialized)
            {
                return false;
            }

            return _registry.UnregisterGroup(
                groupId);
        }

        public bool TryGetGroup(
            string groupId,
            out RuntimeSettings settings)
        {
            if (!IsInitialized)
            {
                settings = null;
                return false;
            }

            return _registry.TryGetGroup(
                groupId,
                out settings);
        }

        public void ResetAll()
        {
            if (!IsInitialized)
            {
                return;
            }

            _registry.ResetAll();
        }
    }
}