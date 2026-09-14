using RuntimeDeveloperToolkit.Core;
using RuntimeDeveloperToolkit.Core.Scheduling;
using RuntimeDeveloperToolkit.Core.Services;
using RuntimeDeveloperToolkit.Performance.Data;
using RuntimeDeveloperToolkit.Settings;
using RuntimeDeveloperToolkit.Performance.Settings;

namespace RuntimeDeveloperToolkit.Performance.Services
{
    /// <summary>
    /// Provides runtime performance monitoring services.
    /// </summary>
    public sealed class RuntimePerformanceService : IRuntimeService
    {
        private RuntimeSettings _settings;
        
        private RuntimeUpdateScheduler _scheduler;
        private RuntimePerformanceSampler _sampler;

        /// <summary>
        /// Gets the service identifier.
        /// </summary>
        public string Id => "performance";

        /// <summary>
        /// Gets whether the service has been initialized.
        /// </summary>
        public bool IsInitialized =>
            _sampler != null;

        /// <summary>
        /// Gets the performance sampler.
        /// </summary>
        public RuntimePerformanceSampler Sampler =>
            _sampler;

        /// <summary>
        /// Gets the latest performance data.
        /// </summary>
        public RuntimePerformanceData CurrentData =>
            new RuntimePerformanceData(
                _sampler?.FPS ?? 0f,
                _sampler?.FrameTimeMilliseconds ?? 0f,
                _sampler?.AverageFPS ?? 0f,
                _sampler?.MinFPS ?? 0f,
                _sampler?.MaxFPS ?? 0f);
        
        public RuntimeSettings Settings => _settings;

        /// <summary>
        /// Initializes the performance service.
        /// </summary>
        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            RuntimeDeveloperToolkitRuntime runtime = RuntimeDeveloperToolkitRuntime.Instance;

            if (runtime == null ||
                !runtime.Services.TryGet<RuntimeSettingsService>(
                    "settings",
                    out RuntimeSettingsService settingsService))
            {
                return;
            }

            _settings = new RuntimeSettings();

            _settings.Register(
                new RuntimeSetting<bool>(
                    "enabled",
                    true));

            _settings.Register(
                new RuntimeSetting<float>(
                    "sampling_interval",
                    0f));

            _settings.Register(
                new RuntimeSetting<int>(
                    "history_capacity",
                    120));

            settingsService.RegisterGroup(
                "performance",
                _settings);
            
            _sampler =
                new RuntimePerformanceSampler();

            _scheduler =
                RuntimeDeveloperToolkit.Scheduler;

            if (_scheduler != null)
            {
                _scheduler.Register(
                    OnUpdate,
                    RuntimeUpdateRate.EveryFrame);
            }
        }

        /// <summary>
        /// Shuts down the performance service.
        /// </summary>
        public void Shutdown()
        {
            if (_scheduler != null)
            {
                _scheduler.Unregister(OnUpdate);
                _scheduler = null;
            }

            _sampler?.Reset();
            _sampler = null;
        }

        private void OnUpdate(
            RuntimeUpdateContext context)
        {
            if (_sampler == null)
            {
                return;
            }

            _sampler.Sample(
                context.DeltaTime);
        }
        
        private int GetHistoryCapacity()
        {
            if (_settings.TryGet<int>(
                    "history_capacity",
                    out RuntimeSetting<int> setting))
            {
                return setting.Value;
            }

            return 120;
        }
        
        public bool IsEnabled
        {
            get
            {
                if (_settings == null)
                {
                    return false;
                }

                if (_settings.TryGet<bool>(
                        "enabled",
                        out RuntimeSetting<bool> setting))
                {
                    return setting.Value;
                }

                return false;
            }
        }

        public float SamplingInterval
        {
            get
            {
                if (_settings == null)
                {
                    return 0f;
                }

                if (_settings.TryGet<float>(
                        "sampling_interval",
                        out RuntimeSetting<float> setting))
                {
                    return setting.Value;
                }

                return 0f;
            }
        }
    }
}