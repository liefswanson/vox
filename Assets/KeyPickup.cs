using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour {

    private bool hasKey = false;
    public Image keyImg;
    public GameObject key;

    void Start()
    {
        keyImg.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            hasKey = true;
            keyImg.enabled = true;
            key.SetActive(false);
        }
    }
}
