using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Activables
{
    public Sprite[] sprites;
    public Sprite[] lightsSprites;
    public SpriteRenderer[] lightsObject;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        LightsUI();
        if (active)
        {
            Open();
        }
        else Close();        
    }

    private void LightsUI()
    {
        if (UseButtonsOnly)
        {
            for (int i = 0; i < lightsObject.Length; i++)
            {
                if (pads[i].active)
                    lightsObject[i].sprite = lightsSprites[1];
                else
                    lightsObject[i].sprite = lightsSprites[0];
            }
        }        
        
    }

    private void Close()
    {
        
        if (UseButtonsOnly)
        {
            for (int i = 0; i < lightsObject.Length; i++)
            {
                lightsObject[i].enabled = true;
            }
        }
        else
        { 
          lightsObject[0].enabled = true;
          
        }
        if (sr.sprite != sprites[0])
        {            
            sr.sprite = sprites[0];
            bc.enabled = true;
        }
    }

    public void Open()
    {
        if (UseButtonsOnly)
        {
            for (int i = 0; i < lightsObject.Length; i++)
            {
                lightsObject[i].enabled = false;
            }
        }
        else
        {
            if (panel.active)
            {
                lightsObject[0].enabled = false;
            }
        }
        
        if (sr.sprite != sprites[1])
        {            
            sr.sprite = sprites[1];
            bc.enabled = false;
        }      
    }    
}
