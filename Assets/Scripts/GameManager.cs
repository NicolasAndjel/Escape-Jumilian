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
    
    // Start is called before the first frame update
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        /*if (p1 == null || p2 == null)
        {
            SceneManager.LoadScene("Level1");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SceneManager.LoadScene("Level1");
        }
    }

}
