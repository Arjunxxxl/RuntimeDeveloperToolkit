using System;
using System.Collections.Generic;

namespace RuntimeDeveloperToolkit.Settings
{
    public sealed class RuntimeSettingsRegistry
    {
        private readonly Dictionary<string, RuntimeSettings> _groups =
            new Dictionary<string, RuntimeSettings>();

        public int Count => _groups.Count;

        public bool RegisterGroup(
            string groupId,
            RuntimeSettings settings)
        {
            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentException(
                    "Settings group ID cannot be null or empty.",
                    nameof(groupId));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (_groups.ContainsKey(groupId))
            {
                return false;
            }

            _groups.Add(groupId, settings);

            return true;
        }

        public bool UnregisterGroup(string groupId)
        {
            if (string.IsNullOrWhiteSpace(groupId))
            {
                return false;
            }

            return _groups.Remove(groupId);
        }

        public bool ContainsGroup(string groupId)
        {
            return !string.IsNullOrWhiteSpace(groupId)
                   && _groups.ContainsKey(groupId);
        }

        public bool TryGetGroup(
            string groupId,
            out RuntimeSettings settings)
        {
            return _groups.TryGetValue(
                groupId,
                out settings);
        }

        public void ResetAll()
        {
            foreach (RuntimeSettings settings in _groups.Values)
            {
                settings.ResetAll();
            }
        }

        public void Clear()
        {
            _groups.Clear();
        }
    }
}