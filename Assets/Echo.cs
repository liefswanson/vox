using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Echo : MonoBehaviour {

    //public AnimationCurve fadeInSlope;
    //public AnimationCurve fadeOutSlope;
    //public float fadeInTime;
    //public float fadeOutTime;

    public string layer;

    public Color color = Color.white;
    [Range(0,8)]
    public float intensity = 1;
    public float echoDistance = 100;

    private Light lightComp;

    private bool echoing = false;

    void Start ()
    {
        lightComp = this.gameObject.AddComponent<Light>();
        lightComp.type = LightType.Point;
        lightComp.color = color;
        lightComp.intensity = 0;
        lightComp.bounceIntensity = 0;
        lightComp.range = echoDistance;
        lightComp.cullingMask = 1 << LayerMask.NameToLayer(layer);
    }

    void Update ()
    {
        //if (CrossPlatformInputManager.GetButtonDown("Echo"))
        //{
        //    echoing = !echoing;
        //}
        
	}

    void LateUpdate()
    {
        if (echoing)
        {
            lightComp.intensity = intensity;
        }
        else
        {
            lightComp.intensity = 0;
        }
    }
}
