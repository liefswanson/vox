using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour {

    public PlatformController target;
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"
            && !activated)
        {
            target.Activate();
            AudioSource source = gameObject.GetComponent<AudioSource>();
            source.Play();
            activated = true;
        }
    }
}
