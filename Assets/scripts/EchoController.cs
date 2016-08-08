﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class EchoController : MonoBehaviour
{

    [Range(0, 1000)]
    public float maximumBreath = 100;
    [Range(0, 1000)]
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
    private float candleRangeTemp;
    public float micScaling = 5;

    private bool yellActive;
    private bool keyActive;

    private bool fading;
    public float fadeSpeed = 1f;

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
        candleRangeRef = candle.range;
    }

    void Update()
    {
        previousLoudness = currentLoudness;
        currentLoudness = micInput.clipLoudness;

        if (CanEcho())
        {
            if (YellEnter() && !keyActive)
            {
                EchoEnter();
                yellActive = true;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Echo") && !yellActive)
            {
                EchoEnter();
                keyActive = true;
            }
        }

        if ((Yelling() || CrossPlatformInputManager.GetButton("Echo"))
            && draining)
        {
            EchoDrain();
            if (yellActive)
            {
                // fix, might need calibration
                // candle.range = candleRangeTemp * candleRangeMult * Mathf.Min(currentLoudness * micScaling, 1);
                candle.range = candleRangeTemp * candleRangeMult;
                candle.range = candleRangeTemp * Mathf.Pow(currentLoudness, 1.0f/4.2f) * (candleRangeMult - 1) + candleRangeTemp; 
            }
            else if (keyActive)
            {
                candle.range = candleRangeTemp * candleRangeMult;
            }
        }
        else
        {
            Rest();
        }

        if (YellExit() || CrossPlatformInputManager.GetButtonUp("Echo") || OutOfBreath())
        {
            EchoExit();
            yellActive = false;
            keyActive = false;
        }

        if (fading)
        {
            candle.range = Mathf.Lerp(candle.range, candleRangeRef, fadeSpeed * Time.deltaTime);
        }

        breathMeter.value = breath;
    }

    private bool YellEnter()
    {
        return currentLoudness >= yellMin
                && previousLoudness < yellMin;
    }

    private bool Yelling()
    {
        return currentLoudness >= yellMin
                && previousLoudness > yellMin;
    }

    private bool YellExit()
    {
        return currentLoudness < yellMin
                && previousLoudness >= yellMin;
    }


    private void EchoEnter()
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
        candleRangeTemp = candleRangeRef;
        fading = false;
    }

    private void EchoDrain()
    {
        breath -= drainRate * Time.deltaTime;
    }

    private void Rest()
    {
        if (breath < maximumBreath)
            breath += rechargeRate * Time.deltaTime;
        if (breath > maximumBreath)
            breath = maximumBreath;
    }

    private void EchoExit()
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
        //candle.range = candleRangeRef;
        fading = true;
    }


    private bool CanEcho()
    {
        return breath >= echoMinimum;
    }

    private bool OutOfBreath()
    {
        return breath <= 0;
    }
}
