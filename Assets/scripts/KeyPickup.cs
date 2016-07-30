using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class KeyPickup : MonoBehaviour {

    private bool hasKey = false;
    public Image keyImg;
    public AudioClip obtainKeyClip;
    private AudioSource source;

    void Start()
    {
        keyImg.enabled = false;
        source = gameObject.GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key"
            && hasKey == false)
        {
            SetHasKey(true);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Door"
           && hasKey == true)
        {
            SetHasKey(false);
            DoorController doorCtrl = other.gameObject.GetComponent<DoorController>();
            doorCtrl.Open();
        }
    }

    void SetHasKey(bool x)
    {
        hasKey = x;
        keyImg.enabled = x;
        if (x)
        {
            source.PlayOneShot(obtainKeyClip);
        }
    }
}
