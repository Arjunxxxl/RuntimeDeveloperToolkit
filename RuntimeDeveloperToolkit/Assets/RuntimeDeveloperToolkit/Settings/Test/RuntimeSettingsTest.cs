using UnityEngine;
using RuntimeDeveloperToolkit.Settings;

public sealed class RuntimeSettingsTest : MonoBehaviour
{
    private void Start()
    {
        RuntimeSettingsService service =
            new RuntimeSettingsService();

        RuntimeSettings performance =
            new RuntimeSettings();

        RuntimeSetting<int> historySize =
            new RuntimeSetting<int>(
                "history_size",
                120);

        RuntimeSetting<bool> showGraph =
            new RuntimeSetting<bool>(
                "show_graph",
                true);

        performance.Register(historySize);
        performance.Register(showGraph);

        bool registered =
            service.RegisterGroup(
                "performance",
                performance);

        Debug.Log(
            $"Performance group registered: {registered}");

        Debug.Log(
            $"Group count: {service.Registry.Count}");

        if (service.TryGetGroup(
                "performance",
                out RuntimeSettings group))
        {
            if (group.TryGet<int>(
                    "history_size",
                    out RuntimeSetting<int> history))
            {
                Debug.Log(
                    $"History size: {history.Value}");
            }

            if (group.TryGet<bool>(
                    "show_graph",
                    out RuntimeSetting<bool> graph))
            {
                Debug.Log(
                    $"Show graph: {graph.Value}");
            }
        }

        historySize.Value = 240;
        showGraph.Value = false;

        Debug.Log(
            $"Updated history size: {historySize.Value}");

        Debug.Log(
            $"Updated show graph: {showGraph.Value}");

        service.ResetAll();

        Debug.Log(
            $"After reset history size: {historySize.Value}");

        Debug.Log(
            $"After reset show graph: {showGraph.Value}");

        service.Clear();

        Debug.Log(
            $"Group count after clear: {service.Registry.Count}");
    }
}