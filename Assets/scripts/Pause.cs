using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Pause : MonoBehaviour {

    private float timeScaleRef;
    private float volumeRef;
    private bool paused;

    public DemoOver demoOver;

    public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
	
	}

    public void PauseOn()
    {
        timeScaleRef = Time.timeScale;
        volumeRef = AudioListener.volume;
        Time.timeScale = 0;
        AudioListener.volume = 0;
        pauseMenu.SetActive(true);

        paused = true;
    }

    public void PauseOff()
    {
        Time.timeScale = timeScaleRef;
        AudioListener.volume = volumeRef;
        pauseMenu.SetActive(false);
        paused = false;
    }
	
	// FIXME
	void Update () {
	    if (CrossPlatformInputManager.GetButtonDown("Pause")
            && !demoOver.paused)
        {
            paused = !paused;
            Cursor.visible = paused;
            if (paused)
            {
                PauseOn();
            }
            else
            {
                PauseOff();
            }
        }
	}
}
