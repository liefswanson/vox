using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    public GameObject player;
    private Image needle;

    void Start()
    {
        needle = GetComponent<Image>();
    }

	void Update () {
        needle.transform.eulerAngles = new Vector3(0, 0, -player.transform.eulerAngles.y);
	}
}
