using System;
using System.Collections.Generic;

namespace RuntimeDeveloperToolkit.Core.Scheduling
{
    public sealed class RuntimeUpdateScheduler
    {
        private readonly List<RuntimeScheduledUpdate> _updates =
            new List<RuntimeScheduledUpdate>();

        private readonly List<RuntimeScheduledUpdate> _pendingAdds =
            new List<RuntimeScheduledUpdate>();

        private readonly List<Action<RuntimeUpdateContext>> _pendingRemovals =
            new List<Action<RuntimeUpdateContext>>();

        private bool _isUpdating;

        public int Count => _updates.Count;

        public bool Register(
            Action<RuntimeUpdateContext> callback,
            RuntimeUpdateRate rate)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (rate == RuntimeUpdateRate.Manual)
            {
                return false;
            }

            if (Contains(callback))
            {
                return false;
            }

            RuntimeScheduledUpdate scheduledUpdate =
                new RuntimeScheduledUpdate(callback, rate);

            if (_isUpdating)
            {
                _pendingAdds.Add(scheduledUpdate);
            }
            else
            {
                _updates.Add(scheduledUpdate);
            }

            return true;
        }

        public bool Unregister(
            Action<RuntimeUpdateContext> callback)
        {
            if (callback == null)
            {
                return false;
            }

            if (_isUpdating)
            {
                if (Contains(callback))
                {
                    _pendingRemovals.Add(callback);
                    return true;
                }

                return false;
            }

            for (int i = 0; i < _updates.Count; i++)
            {
                if (_updates[i].Callback == callback)
                {
                    _updates[i].IsActive = false;
                    _updates.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool ExecuteManual(
            Action<RuntimeUpdateContext> callback,
            RuntimeUpdateContext context)
        {
            if (callback == null)
            {
                return false;
            }

            callback(context);
            return true;
        }

        public void Update(
            float deltaTime,
            float unscaledDeltaTime,
            float elapsedTime,
            float unscaledElapsedTime)
        {
            RuntimeUpdateContext context =
                new RuntimeUpdateContext(
                    deltaTime,
                    unscaledDeltaTime,
                    elapsedTime,
                    unscaledElapsedTime);

            _isUpdating = true;

            for (int i = 0; i < _updates.Count; i++)
            {
                RuntimeScheduledUpdate update = _updates[i];

                if (!update.IsActive)
                {
                    continue;
                }

                if (update.Rate == RuntimeUpdateRate.EveryFrame)
                {
                    Execute(update, context);
                    continue;
                }

                update.Accumulator += unscaledDeltaTime;

                if (update.Accumulator < update.Interval)
                {
                    continue;
                }

                update.Accumulator -= update.Interval;

                Execute(update, context);
            }

            _isUpdating = false;

            ProcessPendingChanges();
        }

        public void Clear()
        {
            for (int i = 0; i < _updates.Count; i++)
            {
                _updates[i].IsActive = false;
            }

            _updates.Clear();
            _pendingAdds.Clear();
            _pendingRemovals.Clear();
        }

        private void Execute(
            RuntimeScheduledUpdate update,
            RuntimeUpdateContext context)
        {
            try
            {
                update.Callback(context);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
        }

        private bool Contains(
            Action<RuntimeUpdateContext> callback)
        {
            for (int i = 0; i < _updates.Count; i++)
            {
                if (_updates[i].Callback == callback)
                {
                    return true;
                }
            }

            for (int i = 0; i < _pendingAdds.Count; i++)
            {
                if (_pendingAdds[i].Callback == callback)
                {
                    return true;
                }
            }

            return false;
        }

        private void ProcessPendingChanges()
        {
            if (_pendingRemovals.Count > 0)
            {
                for (int i = 0; i < _pendingRemovals.Count; i++)
                {
                    Action<RuntimeUpdateContext> callback =
                        _pendingRemovals[i];

                    for (int j = _updates.Count - 1; j >= 0; j--)
                    {
                        if (_updates[j].Callback == callback)
                        {
                            _updates[j].IsActive = false;
                            _updates.RemoveAt(j);
                        }
                    }
                }

                _pendingRemovals.Clear();
            }

            if (_pendingAdds.Count > 0)
            {
                for (int i = 0; i < _pendingAdds.Count; i++)
                {
                    RuntimeScheduledUpdate update = _pendingAdds[i];

                    if (!Contains(update.Callback))
                    {
                        _updates.Add(update);
                    }
                }

                _pendingAdds.Clear();
            }
        }
    }
}