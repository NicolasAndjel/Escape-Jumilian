using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleNear : MonoBehaviour
{
    bool visible;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (visible)
        {
            if (!sr.enabled)
            sr.enabled = true;
        }
        else
        {
            if (sr.enabled)
                sr.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) visible = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) visible = false;
    }


}
