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
                candle.range = candleRangeRef * candleRangeMult * Mathf.Min(currentLoudness * micScaling, 1);
            }
            else if (keyActive)
            {
                candle.range = candleRangeRef * candleRangeMult;
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

        breathMeter.value = Mathf.Floor(breath);
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
        candleRangeRef = candle.range;
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
        candle.range = candleRangeRef;
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
