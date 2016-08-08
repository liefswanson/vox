using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour { 

    public void Restart()
    {
        SceneManager.LoadScene("start");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
