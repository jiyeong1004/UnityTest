using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mic : MonoBehaviour
{
    AudioSource aud;
    //public AudioClip aud;
    int sampleRate = 44100;
    private float[] samples;
    public float rmsValue;
    public float modulate;
    public int resultValue;
    public int cutValue;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
        samples = new float[sampleRate];
        aud.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 100, sampleRate);
    }
    private void Update()
    {
        aud.clip.GetData(samples, 0);  // -1f ~ 1f
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / samples.Length);
        rmsValue = rmsValue * modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, 100);
        resultValue = Mathf.RoundToInt(rmsValue);
        if (resultValue < cutValue)
            resultValue = 0;
    }
    public void PlaySnd()
    {
        aud.Play();
    }
    public void RecSnd()
    {
        aud.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 100, 44100);
    }
}