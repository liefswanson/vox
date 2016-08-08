using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class KeyPickup : MonoBehaviour {

    private bool hasKey = false;
    public Image keyImg;
    public AudioClip obtainKeyClip;
    private AudioSource source;

    void Start()
    {
        keyImg.enabled = false;
        source = GameObject.FindGameObjectsWithTag("Notification")[0].GetComponent<AudioSource>();
        if (source == null)
        {
            throw new System.Exception("Notification object not available. Please put in scene heirarchy.");
        }
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
