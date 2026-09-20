using System;
using System.IO;
using UnityEngine;

public class HighQualityScreenshot : MonoBehaviour
{
    [Header("Capture")]
    public bool TakeScreenshot;

    [Tooltip("1 = Native Resolution, 2 = 2x, 4 = 4x, etc.")]
    [Range(1, 8)]
    public int SuperSize = 4;

    [Header("Save")]
    public string FilePrefix = "Screenshot";

    private void Update()
    {
        if (!TakeScreenshot)
            return;

        TakeScreenshot = false;

        Capture();
    }

    private void Capture()
    {
        string folderPath = Path.Combine(Application.dataPath, "../Screenshots");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        string fileName = $"{FilePrefix}_{timestamp}.png";

        string fullPath = Path.Combine(folderPath, fileName);

        ScreenCapture.CaptureScreenshot(fullPath, SuperSize);

        Debug.Log($"Screenshot saved: {fullPath}");
    }
}