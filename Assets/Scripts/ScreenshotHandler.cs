/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this Code Monkey project
    I hope you find it useful in your own projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour {

    private static ScreenshotHandler instance;
    public int FileCounter = 0;
    public Transform quadArrey;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;

    private void Awake() {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();

        //PlayerPrefs.DeleteKey("photoNum");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot_Static(500, 500);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadImg(); 
        
        }
    }

    private void OnPostRender() {
        if (takeScreenshotOnNextFrame) {
            takeScreenshotOnNextFrame = false;
            
            if (PlayerPrefs.HasKey("photoNum")) ;
                FileCounter = PlayerPrefs.GetInt("photoNum");

            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            Directory.CreateDirectory(Application.streamingAssetsPath + "/Photos");

            File.WriteAllBytes(Application.streamingAssetsPath + "/Photos/" + FileCounter + ".png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");
            FileCounter++;
            PlayerPrefs.SetInt("photoNum", FileCounter);            

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height) {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height) {
        instance.TakeScreenshot(width, height);
    }

    public void LoadImg()
    {       
        for (int i = 0; i < FileCounter + 1; i++)
        {
            byte[] byteTemp = File.ReadAllBytes(Application.streamingAssetsPath + "/Photos/" + i + ".png");
            Texture2D textureTemp = new Texture2D(500, 500);
            textureTemp.LoadImage(byteTemp);
            quadArrey.GetChild(i).GetComponent<Renderer>().material.mainTexture = textureTemp;
        }
    }
}
