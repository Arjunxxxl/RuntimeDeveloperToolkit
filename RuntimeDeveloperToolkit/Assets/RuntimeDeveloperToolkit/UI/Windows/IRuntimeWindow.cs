using UnityEngine;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public interface IRuntimeWindow
    {
        string Id { get; }

        string Title { get; }

        bool IsVisible { get; }

        bool IsInitialized { get; }

        RectTransform Root { get; }

        void Initialize(Transform parent);

        void Show();

        void Hide();

        void SetPosition(Vector2 position);

        void Dispose();
    }
}