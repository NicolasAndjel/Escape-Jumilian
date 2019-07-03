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
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public Boss bossScript;
    
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {        
        
	    if(p1 == null || p2 == null)
		{
			if(!winCanvas.activeInHierarchy)
			{
				loseCanvas.SetActive(true);
			}
		}		
        
        if (boss)    
        {
            if (bossScript.state == Boss.AnimationState.WIN)
            {
                winCanvas.SetActive(true);
                loseCanvas.SetActive(false);
            }                            
            else if (bossScript.state == Boss.AnimationState.LOSE)
            {
                loseCanvas.SetActive(true);
                winCanvas.SetActive(false);
            }
        }

        if (scene.name == "Level2")
        {
            if (!p1.activeInHierarchy && !p2.activeInHierarchy)
            {
                if (!loseCanvas.activeInHierarchy)
                {
                	winCanvas.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
        	if (!loseCanvas.activeInHierarchy)
            	winCanvas.SetActive(true);
        }
    }
    
}
