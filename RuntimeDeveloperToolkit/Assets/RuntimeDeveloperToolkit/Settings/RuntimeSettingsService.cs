using System;

namespace RuntimeDeveloperToolkit.Settings
{
    public sealed class RuntimeSettingsService
    {
        private readonly RuntimeSettingsRegistry _registry;

        public RuntimeSettingsRegistry Registry => _registry;

        public RuntimeSettingsService()
        {
            _registry = new RuntimeSettingsRegistry();
        }

        public bool RegisterGroup(
            string groupId,
            RuntimeSettings settings)
        {
            return _registry.RegisterGroup(
                groupId,
                settings);
        }

        public bool UnregisterGroup(string groupId)
        {
            return _registry.UnregisterGroup(groupId);
        }

        public bool TryGetGroup(
            string groupId,
            out RuntimeSettings settings)
        {
            return _registry.TryGetGroup(
                groupId,
                out settings);
        }

        public void ResetAll()
        {
            _registry.ResetAll();
        }

        public void Clear()
        {
            _registry.Clear();
        }
    }
}