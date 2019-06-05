using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panels : Activables
{
    public GameObject[] activatedUI;

    public override void Start()
    {
        
    }

    public override void Update()
    {        
        if (active)
        {
            Activate();
            active = false;
        }
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

    public void Activate()
    {
        active = true;
        for (int i = 0; i < activatedUI.Length; i++)
        {
            activatedUI[i].SetActive(false);
        }
    }
}
