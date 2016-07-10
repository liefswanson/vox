using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour {

    private bool hasKey = false;
    public Image keyImg;
    public Achieve achieve;

    void Start()
    {
        keyImg.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key"
            && hasKey == false)
        {
            SetHasKey(true);
            other.gameObject.SetActive(false);
            achieve.ObtainKey();
        }
        if (other.gameObject.tag == "Door"
           && hasKey == true)
        {
            SetHasKey(false);
            other.gameObject.SetActive(false);
        }
    }

    void SetHasKey(bool x)
    {
        hasKey = x;
        keyImg.enabled = x;
    }
}
