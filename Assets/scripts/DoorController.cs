using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour {
    private AudioSource aud;
    public AudioClip unlock;
    public AudioClip open;
    private float rotateTime;
    private bool rotating = false;

	void Start () {
        aud = gameObject.GetComponent<AudioSource>();
        rotateTime = open.length;
    }

    void Update()
    {
        if (rotating)
        {
            float temp = Time.deltaTime;
            if (rotateTime < Time.deltaTime)
            {
                temp = rotateTime;
            }

            if (rotateTime <= 0)
            {
                rotating = false;
                Misc.SetLayerRecursively(this.gameObject, LayerMask.NameToLayer("Default"));
                Collider[] colliders = GetComponents<Collider>();
                foreach (var collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else
            {
                transform.RotateAround(transform.Find("hinge").position, Vector3.up, 90 * temp / open.length);
            }
            rotateTime -= Time.deltaTime;
        }
    }

    public void Open()
    {
        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        aud.clip = unlock;
        aud.Play();
        yield return new WaitForSeconds(unlock.length);
        aud.clip = open;
        aud.Play();
        rotating = true;
    }
}
