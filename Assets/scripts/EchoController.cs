using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class EchoController : MonoBehaviour
{

    [Range(0, 100)]
    public float maximumBreath = 100;
    [Range(0, 100)]
    public float echoMinimum = 50;
    public float rechargeRate = 20;
    public float drainRate = 20;

    [Range(0, 1)]
    public float yellMin = 0.5f;

    // public Text breathDisplay;
    public Slider breathMeter;

    public MeshRenderer[] hiddenObjects;

    public Mic micInput;
    private float currentLoudness, previousLoudness;

    private bool draining;
    private float breath;
    private Echo[] echoes;

    public Light candle;
    public float candleRangeMult;
    private float candleRangeRef;
    public float micScaling = 5;

    private bool yellActive;
    private bool keyActive;

    void Start()
    {
        breath = maximumBreath;
        echoes = GetComponentsInChildren<Echo>();

        breathMeter.maxValue = maximumBreath;
        breathMeter.value = maximumBreath;
        for (var i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].enabled = false;
        }
    }

    //FIXME I'm ugly
    void Update()
    {
        previousLoudness = currentLoudness;
        currentLoudness = micInput.clipLoudness;

        // yell
        if (!keyActive)
        {
            if (currentLoudness >= yellMin
                && previousLoudness < yellMin
                && breath >= echoMinimum)
            {
                draining = true;
                for (var i = 0; i < echoes.Length; i++)
                {
                    echoes[i].Begin();
                }
                for (var i = 0; i < hiddenObjects.Length; i++)
                {
                    hiddenObjects[i].enabled = true;
                }
                yellActive = true;
                candleRangeRef = candle.range;
            }
            if (currentLoudness >= yellMin
                && previousLoudness > yellMin
                && draining)
            {
                breath -= drainRate * Time.deltaTime;
                candle.range = candleRangeRef * candleRangeMult * Mathf.Min(currentLoudness * micScaling, 1);
            }
            else
            {
                if (breath < maximumBreath)
                    breath += rechargeRate * Time.deltaTime;
                if (breath > maximumBreath)
                    breath = maximumBreath;
            }
            if (currentLoudness < yellMin
                && previousLoudness >= yellMin
                || breath <= 0)
            {
                draining = false;
                if (breath < 0)
                    breath = 0;
                for (var i = 0; i < echoes.Length; i++)
                {
                    echoes[i].Finish();
                }
                for (var i = 0; i < hiddenObjects.Length; i++)
                {
                    hiddenObjects[i].enabled = false;
                }
                yellActive = false;
                candle.range = candleRangeRef;
            }
        }

        // key press
        if (!yellActive)
        {
            if (CrossPlatformInputManager.GetButtonDown("Echo")
                 && breath >= echoMinimum)
            {
                draining = true;
                for (var i = 0; i < echoes.Length; i++)
                {
                    echoes[i].Begin();
                }
                for (var i = 0; i < hiddenObjects.Length; i++)
                {
                    hiddenObjects[i].enabled = true;
                }
                keyActive = true;
                candleRangeRef = candle.range;
            }
            if (CrossPlatformInputManager.GetButton("Echo")
                && draining)
            {
                breath -= drainRate * Time.deltaTime;
                candle.range = candleRangeRef * candleRangeMult;
            }
            else
            {
                if (breath < maximumBreath)
                    breath += rechargeRate * Time.deltaTime;
                if (breath > maximumBreath)
                    breath = maximumBreath;
            }
            if (CrossPlatformInputManager.GetButtonUp("Echo")
                || breath <= 0)
            {
                draining = false;
                if (breath < 0)
                    breath = 0;
                for (var i = 0; i < echoes.Length; i++)
                {
                    echoes[i].Finish();
                }
                for (var i = 0; i < hiddenObjects.Length; i++)
                {
                    hiddenObjects[i].enabled = false;
                }
                keyActive = false;
                candle.range = candleRangeRef;
            }
        }


        //breathDisplay.text = Mathf.Floor(breath).ToString();
        breathMeter.value = Mathf.Floor(breath);
    }
}
