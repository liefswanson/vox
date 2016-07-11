using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Mic : MonoBehaviour {

    private AudioSource aud;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness {
        get;
        private set;
    }

    private float[] clipSampleData;

    void Start()
    {
        clipSampleData = new float[sampleDataLength];

        aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        aud.loop = true;
        while(!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            aud.clip.GetData(clipSampleData, aud.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        }
    }
}
