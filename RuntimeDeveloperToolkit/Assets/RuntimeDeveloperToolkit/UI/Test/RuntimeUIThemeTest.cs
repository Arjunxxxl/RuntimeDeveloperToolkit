using System.Collections;
using UnityEngine;
using RuntimeDeveloperToolkit;
using RuntimeDeveloperToolkit.Core.Services;
using RuntimeDeveloperToolkit.UI.Services;
using RuntimeDeveloperToolkit.UI.Themes;

namespace RuntimeDeveloperToolkit.UI.Tests
{
    public sealed class RuntimeUIThemeTest : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(UpdateTheme());
        }

        IEnumerator UpdateTheme()
        {
            yield return new WaitForEndOfFrame();
            
            RuntimeServiceRegistry services =
                RuntimeDeveloperToolkit.Services;

            if (services == null)
            {
                Debug.LogError(
                    "RuntimeUIThemeTest: Runtime Service Registry not found.");

                yield break;
            }

            if (!services.TryGet<RuntimeUIService>(
                    "ui",
                    out RuntimeUIService uiService))
            {
                Debug.LogError(
                    "RuntimeUIThemeTest: RuntimeUIService not found.");

                yield break;
            }

            if (uiService.Theme == null)
            {
                Debug.LogError(
                    "RuntimeUIThemeTest: UI Theme is not initialized.");

                yield break;
            }

            RuntimeUIThemeData customTheme =
                RuntimeUIThemeData.CreateDefault();

            customTheme.BackgroundColor =
                new Color(0.3f, 0.05f, 0.05f, 0.95f);

            customTheme.TitleBarColor =
                new Color(0.5f, 0.1f, 0.1f, 1f);

            uiService.Theme.SetTheme(customTheme);

            Debug.Log(
                "RuntimeUIThemeTest: Custom theme applied.");
        }
    }
}