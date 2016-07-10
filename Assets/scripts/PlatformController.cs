using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlatformController : MonoBehaviour {

    public Transform target;
    public AudioClip clipToMatch;
    public AudioClip followUpClip;
    private float speed;
    private bool activated = false;

	void Start () {
        var time = clipToMatch.length - followUpClip.length;
        var distance = Vector3.Distance(transform.position, target.position);
        speed = distance / time;
	}
	
	void Update () {
        if (activated)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
	}

    public void Activate()
    {
        activated = true;
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clipToMatch);
    }
}
