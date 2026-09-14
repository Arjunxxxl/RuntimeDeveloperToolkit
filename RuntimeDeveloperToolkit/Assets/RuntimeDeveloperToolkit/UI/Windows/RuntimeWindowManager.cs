using System;
using System.Collections.Generic;
using RuntimeDeveloperToolkit.UI.Themes;
using UnityEngine;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public sealed class RuntimeWindowManager
    {
        private readonly Dictionary<string, IRuntimeWindow> _windows =
            new Dictionary<string, IRuntimeWindow>();

        private Transform _parent;
        
        private IRuntimeWindow _focusedWindow;

        public IRuntimeWindow FocusedWindow => _focusedWindow;
        public int Count => _windows.Count;
        
        public event Action<IRuntimeWindow> WindowRegistered;
        public event Action<IRuntimeWindow> WindowUnregistered;
        public event Action<IRuntimeWindow> WindowShown;
        public event Action<IRuntimeWindow> WindowHidden;
        
        public IReadOnlyCollection<IRuntimeWindow> VisibleWindows
        {
            get
            {
                List<IRuntimeWindow> visibleWindows =
                    new List<IRuntimeWindow>();

                foreach (IRuntimeWindow window in _windows.Values)
                {
                    if (window.IsVisible)
                        visibleWindows.Add(window);
                }

                return visibleWindows;
            }
        }

        public void Initialize(Transform parent)
        {
            _parent = parent;
        }
        
        public void ApplyTheme(RuntimeUIThemeData theme)
        {
            if (theme == null)
                return;

            foreach (IRuntimeWindow window in _windows.Values)
            {
                if (window is RuntimeWindow runtimeWindow)
                {
                    runtimeWindow.ApplyTheme(theme);
                }
            }
        }

        public bool Register(IRuntimeWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (string.IsNullOrWhiteSpace(window.Id))
            {
                throw new ArgumentException(
                    "Window ID cannot be null or empty.",
                    nameof(window));
            }

            if (_parent == null)
            {
                return false;
            }

            if (_windows.ContainsKey(window.Id))
            {
                return false;
            }

            window.Initialize(_parent);
            
            if (!window.IsInitialized)
                return false;
            
            if (window is RuntimeWindow runtimeWindow)
            {
                runtimeWindow.SetManager(this);
            }
            
            _windows.Add(window.Id, window);
            WindowRegistered?.Invoke(window);

            return true;
        }

        public bool Unregister(string windowId)
        {
            if (string.IsNullOrWhiteSpace(windowId))
            {
                return false;
            }

            if (!_windows.TryGetValue(
                    windowId,
                    out IRuntimeWindow window))
            {
                return false;
            }
            
            if (_focusedWindow == window)
            {
                window.Unfocus();
                _focusedWindow = null;
            }

            window.Dispose();

            _windows.Remove(windowId);
            WindowUnregistered?.Invoke(window);

            return true;
        }

        public bool TryGet(
            string windowId,
            out IRuntimeWindow window)
        {
            return _windows.TryGetValue(
                windowId,
                out window);
        }

        public bool Show(string windowId)
        {
            if (!TryGet(windowId, out IRuntimeWindow window))
            {
                return false;
            }
            
            if (window.IsVisible)
                return true;

            window.Show();
            WindowShown?.Invoke(window);

            return true;
        }

        public bool Hide(string windowId)
        {
            if (!TryGet(windowId, out IRuntimeWindow window))
            {
                return false;
            }
            
            if (!window.IsVisible)
                return true;

            if (_focusedWindow == window)
            {
                window.Unfocus();
                _focusedWindow = null;
            }

            window.Hide();
            WindowHidden?.Invoke(window);

            return true;
        }

        public void HideAll()
        {
            foreach (IRuntimeWindow window in _windows.Values)
            {
                if (_focusedWindow == window)
                {
                    window.Unfocus();
                    _focusedWindow = null;
                }
                
                bool wasVisible = window.IsVisible;
                
                window.Hide();
                
                if (wasVisible)
                {
                    WindowHidden?.Invoke(window);
                }
            }
        }
        
        public bool FocusWindow(string windowId)
        {
            if (string.IsNullOrEmpty(windowId))
                return false;

            if (!_windows.TryGetValue(windowId, out IRuntimeWindow window))
                return false;

            if (!window.IsInitialized)
                return false;
            
            if (!window.IsVisible)
                return false;

            if (_focusedWindow == window)
            {
                window.Focus();
                return true;
            }
            
            if (_focusedWindow != null) 
            {
                _focusedWindow.Unfocus();
                _focusedWindow = null;
            }
            
            _focusedWindow = window;
            window.Focus();
            
            return true;
        }

        public void DisposeAll()
        {
            foreach (IRuntimeWindow window in _windows.Values)
            {
                window.Unfocus(); 
                window.Dispose();
            }

            _focusedWindow = null;
            _windows.Clear();
        }
    }
} 