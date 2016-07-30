using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour {
    private AudioSource aud;

	void Start () {
        aud = gameObject.GetComponent<AudioSource>();
    }

    public void Open()
    {

        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        aud.Play();
        yield return new WaitForSeconds(aud.clip.length);
        this.gameObject.SetActive(false);
    }
}
