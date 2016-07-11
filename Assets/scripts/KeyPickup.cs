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
            StartCoroutine(openDoor(other.gameObject));
        }
    }

    void SetHasKey(bool x)
    {
        hasKey = x;
        keyImg.enabled = x;
    }

    IEnumerator openDoor(GameObject door)
    {
        SetHasKey(false);
        var delay = achieve.OpenDoor();
        yield return new WaitForSeconds(delay);
        door.SetActive(false);
    }
}
