using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour {

    public void Restart()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
