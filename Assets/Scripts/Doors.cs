using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Activables
{
    public Sprite[] sprites;
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
        if (active)
        {
            Open();
        }
        else Close();        
    }

    private void Close()
    {
        if (sr.sprite != sprites[0])
        {
            transform.position = transform.parent.position;
            sr.sprite = sprites[0];
            bc.enabled = true;
        }
    }

    public void Open()
    {
        if (sr.sprite != sprites[1])
        {
            transform.position = transform.parent.position + new Vector3(-0.648f, 0, 0);
            sr.sprite = sprites[1];
            bc.enabled = false;
        }      
    }    
}
