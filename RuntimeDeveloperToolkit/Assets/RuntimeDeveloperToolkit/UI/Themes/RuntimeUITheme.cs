using System;

namespace RuntimeDeveloperToolkit.UI.Themes
{
    public sealed class RuntimeUITheme
    {
        private RuntimeUIThemeData _currentTheme;

        public RuntimeUIThemeData CurrentTheme => _currentTheme;

        public event Action<RuntimeUIThemeData> ThemeChanged;

        public RuntimeUITheme()
        {
            _currentTheme = RuntimeUIThemeData.CreateDefault();
        }

        public void SetTheme(RuntimeUIThemeData theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            if (ReferenceEquals(_currentTheme, theme))
                return;

            _currentTheme = theme;

            ThemeChanged?.Invoke(_currentTheme);
        }

        public void ResetToDefault()
        {
            SetTheme(RuntimeUIThemeData.CreateDefault());
        }
    }
}