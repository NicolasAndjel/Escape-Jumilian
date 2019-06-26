using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class MosquitoBody : MonoBehaviour
{
    
    private Animator anim;
    Rigidbody2D rbMosquito;
    SpriteRenderer srMosquito;
    public AudioSource midgeAud;
    public float speed;
    public Transform target;
    public float deathTime;
    bool isDead;
    Vector3 mosquitoPosition;
    public AudioClip poof;


    void Start()
    {
        anim = GetComponent<Animator>();
        srMosquito = GetComponent<SpriteRenderer>();
        rbMosquito = GetComponent<Rigidbody2D>();
        midgeAud = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    void Update()
    {

        if (!isDead)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.position += speed * direction * Time.deltaTime;
            if (target.transform.position.x > rbMosquito.transform.position.x)
            {
                srMosquito.flipX = true;
            }
            else
            {
                srMosquito.flipX = false;
            }

        }
        else
        {
            deathTime += 0.5f;
            if (deathTime > 40)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            midgeAud.volume = 0.3f;
            midgeAud.PlayOneShot(poof);
            GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
            anim.Play("FlyDead");

        }
        if (collision.gameObject.layer == 0)
        {
            midgeAud.volume = 0.3f;
            midgeAud.PlayOneShot(poof);
            GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
            anim.Play("FlyDead");
        }
    }
}
