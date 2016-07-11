using UnityEngine;
using System.Collections;

public class DemoOver : MonoBehaviour {

    private float timeScaleRef;
    private float volumeRef;
    public bool paused
    {
        get;
        private set;
    }

    public GameObject demoOverMenu;

    public void DemoOverMenuOn()
    {
        timeScaleRef = Time.timeScale;
        volumeRef = AudioListener.volume;
        Time.timeScale = 0;
        AudioListener.volume = 0;
        demoOverMenu.SetActive(true);

        paused = true;
    }

    public void DemoOverMenuOff()
    {
        Time.timeScale = timeScaleRef;
        AudioListener.volume = volumeRef;
        demoOverMenu.SetActive(false);
        paused = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DemoOverMenuOn();
        }
    }
}
