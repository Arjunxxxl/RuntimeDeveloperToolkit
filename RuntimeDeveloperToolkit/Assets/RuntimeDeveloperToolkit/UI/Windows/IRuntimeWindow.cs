using UnityEngine;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public interface IRuntimeWindow
    {
        string Id { get; }

        string Title { get; }

        bool IsVisible { get; }
        
        bool IsFocused { get; }

        bool IsInitialized { get; }

        RectTransform Root { get; }
        
        RectTransform ContentRoot { get; }

        void Initialize(Transform parent);

        void Show();

        void Hide();
        
        void Focus();
        
        void Unfocus();
        
        void ClearContent();

        void SetPosition(Vector2 position);

        void Dispose();
    }
}