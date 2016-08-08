using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    void OnTriggerExit(Collider Other)
    {
        //Light[] lights = GetComponentsInChildren<Light>();
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
