using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Achieve : MonoBehaviour {
    private AudioSource source;
    public AudioClip obtainKeyClip;

    public void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void ObtainKey()
    {
        source.PlayOneShot(obtainKeyClip);
    }

    public void PressButton()
    {

    }
}
