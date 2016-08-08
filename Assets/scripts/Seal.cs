using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Seal : MonoBehaviour {
    public PlatformController pc;
    private AudioSource aud;
    // Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (pc.done)
        {
            aud.mute = true;
        }
	}
}
