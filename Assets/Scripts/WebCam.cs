using UnityEngine;
using System.Collections;
using UnityEngine.Android;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;
    public GameObject ppVolumeHigh;
    public GameObject ppVolumeMed;
    public GameObject ppVolumeLow;
    public GameObject startButton;
    private PostProcessVolume ppHigh;
    private PostProcessVolume ppMed;
    private PostProcessVolume ppLow;
    void Start(){
        ppHigh = ppVolumeHigh.GetComponent<PostProcessVolume>();
        ppMed = ppVolumeMed.GetComponent<PostProcessVolume>();
        ppLow = ppVolumeLow.GetComponent<PostProcessVolume>();
    }
    public void SwapCam_Clicked()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;

            if (tex != null)
            {
                StopWebCam();
                StartStopCam_Clicked();
            }
        }
    }

    public void firstStart(){ 
        #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera)){
                Permission.RequestUserPermission(Permission.Camera);
                return; 
            }
        #endif
        startButton.SetActive(false);
        StartStopCam_Clicked();
    }

    public void StartStopCam_Clicked()
    {
        if (tex != null) // Stop the camera
        {
            StopWebCam();
        }
        else // Start the camera
        {
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;

            tex.Play();
        }
    }

    public void disableNoise(){
        ppHigh.enabled = false;
        ppMed.enabled = false;
        ppLow.enabled = false;
        
    }
    
    
    public void lowNoise(){
        ppHigh.enabled = false;
        ppMed.enabled = false;
        ppLow.enabled = true;
        
    }
    public void mediumNoise(){
        ppHigh.enabled = false;
        ppMed.enabled = true;
        ppLow.enabled = false;
        
    }
    public void highNoise(){
        ppHigh.enabled = true;
        ppMed.enabled = false;
        ppLow.enabled = false;
    }
    private void StopWebCam()
    {
        display.texture = null;
        tex.Stop();
        tex = null;
    }
}
