using UnityEngine;

namespace RuntimeDeveloperToolkit.UI.Themes
{
    public sealed class RuntimeUIThemeData
    {
        // General
        public Color BackgroundColor { get; set; }
        public Color PanelColor { get; set; }
        public Color TitleBarColor { get; set; }

        // Text
        public Color TextColor { get; set; }
        public Color SecondaryTextColor { get; set; }

        // Interactive elements
        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Color DisabledColor { get; set; }

        // Fonts
        public int TitleFontSize { get; set; }
        public int BodyFontSize { get; set; }
        public int SmallFontSize { get; set; }

        public static RuntimeUIThemeData CreateDefault()
        {
            return new RuntimeUIThemeData
            {
                BackgroundColor =
                    new Color(0.08f, 0.08f, 0.08f, 0.95f),

                PanelColor =
                    new Color(0.12f, 0.12f, 0.12f, 1f),

                TitleBarColor =
                    new Color(0.16f, 0.16f, 0.16f, 1f),

                TextColor =
                    Color.white,

                SecondaryTextColor =
                    new Color(0.7f, 0.7f, 0.7f, 1f),

                PrimaryColor =
                    new Color(0.2f, 0.6f, 1f, 1f),

                SecondaryColor =
                    new Color(0.25f, 0.25f, 0.25f, 1f),

                DisabledColor =
                    new Color(0.35f, 0.35f, 0.35f, 1f),

                TitleFontSize = 16,
                BodyFontSize = 14,
                SmallFontSize = 12
            };
        }
    }
}