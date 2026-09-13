using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimeDeveloperToolkit.UI.Windows
{
    public sealed class RuntimeWindowManager
    {
        private readonly Dictionary<string, IRuntimeWindow> _windows =
            new Dictionary<string, IRuntimeWindow>();

        private Transform _parent;

        public int Count => _windows.Count;

        public void Initialize(Transform parent)
        {
            _parent = parent;
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

            _windows.Add(window.Id, window);

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

            window.Dispose();

            _windows.Remove(windowId);

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

            window.Show();

            return true;
        }

        public bool Hide(string windowId)
        {
            if (!TryGet(windowId, out IRuntimeWindow window))
            {
                return false;
            }

            window.Hide();

            return true;
        }

        public void HideAll()
        {
            foreach (IRuntimeWindow window in _windows.Values)
            {
                window.Hide();
            }
        }

        public void DisposeAll()
        {
            foreach (IRuntimeWindow window in _windows.Values)
            {
                window.Dispose();
            }

            _windows.Clear();
        }
    }
} 