namespace RuntimeDeveloperToolkit.Core.Services
{
    public interface IRuntimeService
    {
        string Id { get; }

        bool IsInitialized { get; }

        void Initialize();

        void Shutdown();
    }
} 