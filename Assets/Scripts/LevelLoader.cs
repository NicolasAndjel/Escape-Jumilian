using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{    

    public void Close()
    {
        Application.Quit();
    }
    public void CargarNivel(string name)
    {     
        SceneManager.LoadScene(name);
    }
}
