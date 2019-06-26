using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FollowForCamera mainCamera;
    public EnemySpawner spawner;
    public GameObject p1;
    public GameObject p2;
    public List<EnemiesHabilities> aliens;
    public Scene scene;
    public bool boss;
    
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {        
        if (!boss)
        {

        }
        if (scene.name == "Level2")
        {
            if (!p1.activeInHierarchy && !p2.activeInHierarchy)
            {
                SceneManager.LoadScene("Boss");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    
}
