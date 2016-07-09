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
    public Text breathDisplay;

    private bool draining;

    private float breath;

	void Start () {
        breath = maximumBreath;
        breathDisplay.text = Mathf.Floor(breath).ToString();
	}
	
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Echo")
            && breath >= echoMinimum)
            draining = true;

        if (CrossPlatformInputManager.GetButton("Echo")
            && draining)
            breath -= drainRate * Time.deltaTime;
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
        }
        breathDisplay.text = Mathf.Floor(breath).ToString();
    }
}
