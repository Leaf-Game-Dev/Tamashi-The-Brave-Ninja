using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenCamShake : MonoBehaviour
{
    public static ScreenCamShake Instance { get; private set; }



    CinemachineVirtualCamera Vcam;

    private float shakeTimer,StartingIntensity,shakeTimeTotal;

    private void Awake()
    {
        Instance = this;
        Vcam = GetComponent<CinemachineVirtualCamera>();

    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CamBMP = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CamBMP.m_AmplitudeGain = intensity;
        StartingIntensity = intensity;
        shakeTimeTotal = time;
        shakeTimer = time;

    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin CamBMP = Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CamBMP.m_AmplitudeGain = Mathf.Lerp(StartingIntensity,0f,1-(shakeTimer/shakeTimeTotal));
            }
        }
    }
}
