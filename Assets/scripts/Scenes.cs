using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("start");
    }
}
