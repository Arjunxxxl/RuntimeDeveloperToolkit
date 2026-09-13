using System;
using System.Collections.Generic;

namespace RuntimeDeveloperToolkit.Core.Services
{
    public sealed class RuntimeServiceRegistry
    {
        private readonly Dictionary<string, IRuntimeService> _services =
            new Dictionary<string, IRuntimeService>();

        public int Count => _services.Count;

        public bool Register(IRuntimeService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (string.IsNullOrWhiteSpace(service.Id))
            {
                throw new ArgumentException(
                    "Service ID cannot be null or empty.",
                    nameof(service));
            }

            if (_services.ContainsKey(service.Id))
            {
                return false;
            }

            _services.Add(service.Id, service);

            return true;
        }

        public bool Unregister(string serviceId)
        {
            if (string.IsNullOrWhiteSpace(serviceId))
            {
                return false;
            }

            return _services.Remove(serviceId);
        }

        public bool Contains(string serviceId)
        {
            return !string.IsNullOrWhiteSpace(serviceId)
                   && _services.ContainsKey(serviceId);
        }

        public bool TryGet(
            string serviceId,
            out IRuntimeService service)
        {
            return _services.TryGetValue(
                serviceId,
                out service);
        }

        public bool TryGet<T>(
            string serviceId,
            out T service)
            where T : class, IRuntimeService
        {
            if (_services.TryGetValue(
                    serviceId,
                    out IRuntimeService registeredService))
            {
                service = registeredService as T;

                return service != null;
            }

            service = null;

            return false;
        }

        public void InitializeAll()
        {
            foreach (IRuntimeService service in _services.Values)
            {
                if (!service.IsInitialized)
                {
                    service.Initialize();
                }
            }
        }

        public void ShutdownAll()
        {
            foreach (IRuntimeService service in _services.Values)
            {
                if (service.IsInitialized)
                {
                    service.Shutdown();
                }
            }
        }

        public void Clear()
        {
            _services.Clear();
        }
    }
}