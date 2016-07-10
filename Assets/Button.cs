using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public PlatformController target;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target.Activate();
            Debug.Log("derp");
        }
    }
}
