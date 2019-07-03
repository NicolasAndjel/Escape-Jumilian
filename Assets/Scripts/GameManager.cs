using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject pauseCanvas;
    public Boss bossScript;
    public List<MonoBehaviour> mns;
    public MonoBehaviour[] mnsArray;
    public Button level1b;
    bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        mns = new List<MonoBehaviour>();
        mnsArray = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < mnsArray.Length; i++)
        {
            if ((mnsArray[i].gameObject.tag != "MainScript"))
            {
                mns.Add(mnsArray[i]);
            }
        }
        scene = SceneManager.GetActiveScene();
        mnsArray = null;
    }

    // Update is called once per frame
    void Update()
    {              
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                for (int i = 0; i < mns.Count; i++)
                {
                    mns[i].enabled = false;
                }
                paused = true;
                pauseCanvas.SetActive(true);
                level1b.Select();
            }
            else
            {
                for (int i = 0; i < mns.Count; i++)
                {
                    mns[i].enabled = true;
                }
                paused = false;
                pauseCanvas.SetActive(false);
            }            
        }
        
	    if(p1 == null || p2 == null)
		{
            for (int i = 0; i < mns.Count; i++)
            {
                mns[i].enabled = false;
            }
            if (!winCanvas.activeInHierarchy)
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
            for (int i = 0; i < mns.Count; i++)
            {
                mns[i].enabled = false;
            }
            if (!loseCanvas.activeInHierarchy)
            	winCanvas.SetActive(true);
        }
    }
    
}
