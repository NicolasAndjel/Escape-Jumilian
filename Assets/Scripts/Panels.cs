using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : Activables
{
    public GameObject[] activatedUI;
    public SpriteRenderer lights;
    public Sprite lightSpriteActive;
    public Sprite startSprite;
    

    public override void Start()
    {
        startSprite = lights.sprite;
    }

    public override void Update()
    {
        if (IsActive())
        {
            Activate();
        }
        else Deactivate();
    }

    public bool IsActive()
    {
        return active;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            Activate();
        }
    }

    public void Deactivate()
    {
        active = false;
        for (int i = 0; i < activatedUI.Length; i++)
        {
            activatedUI[i].SetActive(false);
        }
        lights.sprite = startSprite;
    }

    public void Activate()
    {
        active = true;
        for (int i = 0; i < activatedUI.Length; i++)
        {
            activatedUI[i].SetActive(true);
        }
        lights.sprite = lightSpriteActive;
    }
}
