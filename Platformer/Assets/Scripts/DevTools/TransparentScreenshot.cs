using UnityEngine;
using System.IO;

public class TransparentScreenshot : MonoBehaviour
{
    public Camera screenshotCamera; // Assign your scene camera
    public int screenshotWidth = 1920;
    public int screenshotHeight = 1080;
    public KeyCode captureKey = KeyCode.F12; // Press F12 to capture

    private void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            StartCoroutine(CaptureTransparentScreenshot());
        }
    }

    private System.Collections.IEnumerator CaptureTransparentScreenshot()
    {
        // Create a temporary RenderTexture
        RenderTexture renderTexture = new RenderTexture(screenshotWidth, screenshotHeight, 24, RenderTextureFormat.ARGB32);
        screenshotCamera.targetTexture = renderTexture;
        screenshotCamera.backgroundColor = new Color(0, 0, 0, 0); // Fully transparent
        screenshotCamera.clearFlags = CameraClearFlags.SolidColor;

        // Render the camera's view
        screenshotCamera.Render();

        // Read the pixels into a Texture2D
        Texture2D screenshot = new Texture2D(screenshotWidth, screenshotHeight, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, screenshotWidth, screenshotHeight), 0, 0);
        screenshot.Apply();

        // Reset camera
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Encode to PNG
        byte[] pngData = screenshot.EncodeToPNG();
        Destroy(screenshot);

        // Save the file
        string folderPath = Application.dataPath + "/TransparentScreenshots";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, "TransparentScreenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
        File.WriteAllBytes(filePath, pngData);
        Debug.Log("Screenshot saved: " + filePath);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); // Refresh Unity to show the new file
#endif

        yield return null;
    }
}
