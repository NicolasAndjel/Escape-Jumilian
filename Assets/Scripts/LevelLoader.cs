using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{    

    public void hola()
    {
        Debug.Log("hola");
    }
    public void CargarNivel(string name)
    {     
        SceneManager.LoadScene(name);
    }
}
