using System.IO;
using UnityEditor;
using UnityEngine;

public class TopBarScreenshot
{
    [MenuItem("Hell/Take Screenshot %#k")] 
    public static void CaptureScreen()
    {
        string folderPath = Path.Combine(Application.dataPath, "../Screenshots");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = $"Screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string fullPath = Path.Combine(folderPath, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log($"Screenshot saved to: {fullPath}");
    }
}