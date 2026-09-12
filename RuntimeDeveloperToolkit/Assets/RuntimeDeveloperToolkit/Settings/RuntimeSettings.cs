using System;
using System.Collections.Generic;

namespace RuntimeDeveloperToolkit.Settings
{
    public sealed class RuntimeSettings
    {
        private readonly Dictionary<string, IRuntimeSetting> _settings =
            new Dictionary<string, IRuntimeSetting>();

        public int Count => _settings.Count;

        public bool Register<T>(RuntimeSetting<T> setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            if (_settings.ContainsKey(setting.Id))
            {
                return false;
            }

            _settings.Add(setting.Id, setting);

            return true;
        }

        public bool Unregister(string settingId)
        {
            if (string.IsNullOrWhiteSpace(settingId))
            {
                return false;
            }

            return _settings.Remove(settingId);
        }

        public bool Contains(string settingId)
        {
            return !string.IsNullOrWhiteSpace(settingId)
                   && _settings.ContainsKey(settingId);
        }

        public bool TryGet<T>(
            string settingId,
            out RuntimeSetting<T> setting)
        {
            if (_settings.TryGetValue(
                    settingId,
                    out IRuntimeSetting value))
            {
                setting = value as RuntimeSetting<T>;

                return setting != null;
            }

            setting = null;

            return false;
        }

        public void ResetAll()
        {
            foreach (IRuntimeSetting setting in _settings.Values)
            {
                setting.Reset();
            }
        }
    }
}