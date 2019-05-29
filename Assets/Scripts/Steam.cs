using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public float TimeActive;
    public float damage;
    private SpriteRenderer sr;
    BoxCollider2D bc;
    private float timer;
    bool active;
    public bool fading;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > TimeActive)
        {
            if (fading)
            {
                sr.color = FadeAway(sr.color.a, 0);
                if (sr.color.a < 0.1)
                {
                    sr.color = new Color(1, 1, 1, 0);
                    timer = 0;
                    bc.enabled = false;
                    fading = false;
                }
            }
            else
            {
                sr.color = FadeAway(sr.color.a, 1);
                if (sr.color.a > 0.9)
                {
                    sr.color = new Color(1, 1, 1, 1);
                    timer = 0;
                    bc.enabled = true;
                    fading = true;
                }
            }
            
        }
    }

    private void DoDamage(float amount, GameObject go)
    {
        go.GetComponent<Damaggeable>().GetDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            DoDamage(damage,collision.gameObject);
            collision.gameObject.GetComponent<HerosMovement>().KnockUp();
        }
    }

    Color FadeAway(float from, float to)
    {

        return new Color(1, 1, 1, Mathf.Lerp(from, to, Time.deltaTime)); ;
    }
}
