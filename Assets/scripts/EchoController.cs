using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class EchoController : MonoBehaviour {

    [Range(0,100)]
    public float maximumBreath = 100;
    [Range(0,100)]
    public float echoMinimum = 50;
    public float rechargeRate = 20;
    public float drainRate = 20;
    // public Text breathDisplay;
    public Slider breathMeter;

    public MeshRenderer[] hiddenObjects;

    private bool draining;
    private float breath;
    private Echo[] echoes;

	void Start () {
        breath = maximumBreath;
        //breathDisplay.text = Mathf.Floor(breath).ToString();
        echoes = GetComponentsInChildren<Echo>();

        breathMeter.maxValue = maximumBreath;
        breathMeter.value = maximumBreath;
        for (var i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].enabled = false;
        }
    }
	
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Echo")
            && breath >= echoMinimum)
        {
            draining = true;
            for(var i = 0; i < echoes.Length; i++)
            {
                echoes[i].Begin();
            }
            for(var i = 0; i < hiddenObjects.Length; i++)
            {
                hiddenObjects[i].enabled = true;
            }
        }
        if (CrossPlatformInputManager.GetButton("Echo")
            && draining)
        {
            breath -= drainRate * Time.deltaTime;
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
        }
        //breathDisplay.text = Mathf.Floor(breath).ToString();
        breathMeter.value = Mathf.Floor(breath);
    }
}
