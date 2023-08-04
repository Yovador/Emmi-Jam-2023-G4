using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tool_Screen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Capture(GetComponent<Camera>(), 7559, 3779);
            ScreenCapture.CaptureScreenshot("screeen.png");
        }
    }

    /// <summary>
    /// Create a custom resolution screenshot using the specified camera.
    /// The resolution can be higher than the screen size.
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void Capture(Camera camera, int width, int height)
    {

        // save data which we'll modify
        RenderTexture prevRenderTexture = RenderTexture.active;
        RenderTexture prevCameraTargetTexture = camera.targetTexture;
        bool prevCameraEnabled = camera.enabled;

        // create rendertexture
        int msaaSamples = 1;
        RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, msaaSamples);

        try
        {
            // disabling the camera is important, otherwise you get e. g. a blurry image with different focus than the one the camera displays
            // see https://docs.unity3d.com/ScriptReference/Camera.Render.html
            camera.enabled = false;

            // set rendertexture into which the camera renders
            camera.targetTexture = renderTexture;

            // render a single frame
            camera.Render();

            // create image using the camera's render texture
            RenderTexture.active = camera.targetTexture;

            Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenShot.Apply();

            // save the image
            byte[] bytes = screenShot.EncodeToPNG();

            string filepath = Path.Combine("/", "screen");

            System.IO.File.WriteAllBytes(filepath, bytes);

            Debug.Log(string.Format("[<color=blue>Screenshot</color>]Screenshot captured\n<color=grey>{0}</color>", filepath));

        }
        catch (Exception ex)
        {
            Debug.LogError("Screenshot capture exception: " + ex);
        }
        finally
        {
            RenderTexture.ReleaseTemporary(renderTexture);

            // restore modified data
            RenderTexture.active = prevRenderTexture;
            camera.targetTexture = prevCameraTargetTexture;
            camera.enabled = prevCameraEnabled;

        }

    }

}
