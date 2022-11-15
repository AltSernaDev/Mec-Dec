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
    private Camera main_;
    private RenderTexture textureCam;
    private bool takeScreenshotOnNextFrame;

    [SerializeField] bool reset_;

    private void Awake() {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        main_ = Camera.main;
        textureCam = myCamera.targetTexture;

        if (reset_)
            PlayerPrefs.DeleteKey("photoNum");
    }
    private void Update()
    {    
        if (Input.GetKeyDown(KeyCode.Space) && myCamera.gameObject.activeSelf)
        {
            if (!(LifeTimeMananger.instance.DaysPassed.Days >= (int)(FileCounter / 3)))
            {
                TakeScreenshot_Static(500, 500);
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadImg();         
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnPostRender() {
        if (takeScreenshotOnNextFrame) {
            takeScreenshotOnNextFrame = false;
            
            if (PlayerPrefs.HasKey("photoNum")) 
                FileCounter = PlayerPrefs.GetInt("photoNum");

            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            Directory.CreateDirectory(Application.streamingAssetsPath + "/Photos");

            File.WriteAllBytes(Application.streamingAssetsPath + "/Photos/" + LifeTimeMananger.instance.CurrentHour.Day.ToString() + "-" + LifeTimeMananger.instance.CurrentHour.Month.ToString() + "-2004_" + (FileCounter + 1) + ".png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");
            FileCounter++;
            PlayerPrefs.SetInt("photoNum", FileCounter);            

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
            
            myCamera.gameObject.SetActive(false);
            Invoke("ReActiveCamera", 1f);
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
            byte[] byteTemp = File.ReadAllBytes(Application.streamingAssetsPath + (LifeTimeMananger.instance.StartDay.AddDays((int)(i / 3)).Day) + "-" + (LifeTimeMananger.instance.StartDay.AddDays((int)(i / 3)).Month).ToString() + "-2004_" + (i + 1) + ".png");
            Texture2D textureTemp = new Texture2D(500, 500);
            textureTemp.LoadImage(byteTemp);
            quadArrey.GetChild(i).GetComponent<Renderer>().material.mainTexture = textureTemp;
        }
    }

    void ReActiveCamera()
    {
        myCamera.gameObject.SetActive(true);
        myCamera.targetTexture = textureCam;
    }
}
