using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FallDie : MonoBehaviour {

    private float timeScaleRef;
    private float volumeRef;
    public bool paused
    {
        get;
        private set;
    }

    public GameObject deathMenu;

    public void DeathMenuOn()
    {
        timeScaleRef = Time.timeScale;
        volumeRef = AudioListener.volume;
        Time.timeScale = 0;
        AudioListener.volume = 0;
        deathMenu.SetActive(true);
        paused = true;
    }

    public void DeathMenuOff()
    {
        Time.timeScale = timeScaleRef;
        AudioListener.volume = volumeRef;
        deathMenu.SetActive(false);
        paused = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var scene = SceneManager.GetActiveScene().name;
            DeathMenuOn();
            //SceneManager.LoadScene(scene);
        }
    }
}
