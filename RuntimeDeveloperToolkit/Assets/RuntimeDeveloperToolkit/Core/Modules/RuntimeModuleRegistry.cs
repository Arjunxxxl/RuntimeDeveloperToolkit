using System;
using System.Collections.Generic;

namespace RuntimeDeveloperToolkit.Core.Modules
{
    /// <summary>
    /// Stores and manages registered runtime modules.
    /// </summary>
    public sealed class RuntimeModuleRegistry
    {
        private readonly Dictionary<string, IRuntimeModule> _modules =
            new Dictionary<string, IRuntimeModule>();

        /// <summary>
        /// Gets the number of registered modules.
        /// </summary>
        public int Count => _modules.Count;

        /// <summary>
        /// Registers a module.
        /// </summary>
        public bool Register(IRuntimeModule module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            if (string.IsNullOrWhiteSpace(module.Id))
            {
                throw new ArgumentException(
                    "Module ID cannot be null or empty.",
                    nameof(module));
            }

            if (_modules.ContainsKey(module.Id))
            {
                return false;
            }

            _modules.Add(module.Id, module);

            return true;
        }

        /// <summary>
        /// Unregisters a module.
        /// </summary>
        public bool Unregister(string moduleId)
        {
            if (string.IsNullOrWhiteSpace(moduleId))
            {
                return false;
            }

            return _modules.Remove(moduleId);
        }

        /// <summary>
        /// Gets a module by its ID.
        /// </summary>
        public bool TryGet(
            string moduleId,
            out IRuntimeModule module)
        {
            return _modules.TryGetValue(moduleId, out module);
        }

        /// <summary>
        /// Checks whether a module is registered.
        /// </summary>
        public bool Contains(string moduleId)
        {
            return !string.IsNullOrWhiteSpace(moduleId)
                   && _modules.ContainsKey(moduleId);
        }

        /// <summary>
        /// Initializes all registered modules.
        /// </summary>
        public void InitializeAll()
        {
            foreach (IRuntimeModule module in _modules.Values)
            {
                module.Initialize();
            }
        }

        /// <summary>
        /// Enables all registered modules.
        /// </summary>
        public void EnableAll()
        {
            foreach (IRuntimeModule module in _modules.Values)
            {
                module.Enable();
            }
        }

        /// <summary>
        /// Disables all registered modules.
        /// </summary>
        public void DisableAll()
        {
            foreach (IRuntimeModule module in _modules.Values)
            {
                module.Disable();
            }
        }

        /// <summary>
        /// Disposes all registered modules.
        /// </summary>
        public void DisposeAll()
        {
            foreach (IRuntimeModule module in _modules.Values)
            {
                module.Dispose();
            }

            _modules.Clear();
        }
    }
} 