using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using Action = System.Action;

public class QRScanner : MonoBehaviour
{
    [SerializeField] private RawImage rendererWebcam;
    WebCamTexture webcamTexture;
    string QrCode = string.Empty;
    private System.Action<string> qrCodeCompleted;

    void Start()
    {
        
    }

    public void StartScanner(System.Action<string> qrCodeCompleted)
    {
        webcamTexture = new WebCamTexture(512, 512);
        rendererWebcam.material.mainTexture = webcamTexture;
        this.qrCodeCompleted = qrCodeCompleted;
        StartCoroutine(GetQRCode());
    }

    public void StopScanner()
    {
        webcamTexture.Stop();
    }
    

    IEnumerator GetQRCode()
    {
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        qrCodeCompleted?.Invoke(QrCode);
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
        webcamTexture.Stop();
    }
    
   
}
