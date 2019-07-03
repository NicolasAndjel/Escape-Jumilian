using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum AnimationState
    {
        ATTACK,
        GETHIT,
        IDLE,
        DEATH,
        STEP,
        WIN,
        LOSE
    }

    public SpriteRenderer sr;
    public GameObject p1;
    public GameObject p2;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public Animator[] shield;
    public float life;
    public AnimationState state;
    public Transform spawn;
    public GameObject bullet;
    public GameObject muzzle;
    public float TimeBtwShots;
    public float shotsTimer;
    public Animator anim;
    public float animatorTimer;
    public float animTime;
    public bool didGetTime;
    public GameObject redEnemy;
    public GameObject redEnemyInGame;
    public Activables[] panel;
    public ElementalActivator ea;
    public Animator[] anims;
    public Active ac;
    public bool CanGetDamage;
    public Vector3 redStartPosition;
    public Transform box;
    public Vector3 startBoxPosition;
    public float timeToAttack;
    float counter;
    float colorCounter;
    public AudioClip[] aC;
    public AudioSource aS;
    bool done;

    void Start()
    {
        colorCounter = 0;
        startBoxPosition = box.position;
        redStartPosition = redEnemyInGame.transform.parent.position;
        state = AnimationState.IDLE;
        shotsTimer = 0;
        aS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void GetAnimTime()
    {
        AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        animTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;        
    }

    void Update()
    {

        if (state != AnimationState.WIN)
        {
            if (p1 == null || p2 == null)
            {
                state = AnimationState.LOSE;
            }
        }    	
    	if (ac.active)
        {
            counter = 0;
            done = false;
            aS.volume = 1;
            aS.Stop();
            state = AnimationState.GETHIT;           
            for (int i = 0; i < anims.Length; i++)
            {
                anims[i].SetBool("Active", false);
            }
        }

        if (life == 0)
        {
            state = AnimationState.DEATH;
        }

        if (state == AnimationState.IDLE)
        {
            if (!aS.isPlaying)
            {
                aS.PlayOneShot(aC[3]);
            }
            if (colorCounter > 1)
            {
                sr.color = Color.white;
            }
            colorCounter += Time.deltaTime;
            sr.color = Color.white;
            counter += Time.deltaTime;
            if (counter > timeToAttack)
            {
                colorCounter = 0;
                state = AnimationState.STEP;
                counter = 0;
                aS.PlayOneShot(aC[4]);
            }
        }
        else if (state == AnimationState.STEP)
        {
            if (!aS.isPlaying)
            {
                aS.PlayOneShot(aC[0]);
            }
            anim.SetBool("Step", true);
            if (!didGetTime)
            {
                GetAnimTime();
                if (animTime > 3)
                {
                    animTime = 3;
                }
                didGetTime = true;
            }
            animatorTimer += Time.deltaTime;
            if (animatorTimer > animTime + 1.5f)
            {
                anim.SetBool("Attaking", true);
                state = AnimationState.ATTACK;
                didGetTime = false;
                animatorTimer = 0;
            }
        }
        else if (state == AnimationState.ATTACK)
        {
            if (!done)
            {
                aS.volume = 0.5f;
                aS.PlayOneShot(aC[1]);
                done = true;
            }
            if (!aS.isPlaying)
            {
                aS.volume = 1;
            }
            anim.SetBool("Step", false);
            if (!didGetTime)
            {
                GetAnimTime();
                didGetTime = true;
            }
            Shot();
            animatorTimer += Time.deltaTime;
            if (animatorTimer > 6)
            {
                anim.SetBool("Attaking", false);
                state = AnimationState.IDLE;
                didGetTime = false;
                animatorTimer = 0;
                shotsTimer = 0;
                done = false;
            }
        }
        else if (state == AnimationState.GETHIT)
        {
            if (!done)
            {
                aS.PlayOneShot(aC[2]);
            }
            anim.SetBool("Hitted", true);
            if (!didGetTime)
            {
                GetAnimTime();
                didGetTime = true;
            }
            animatorTimer += Time.deltaTime;
            GetDamagge();
            if (animatorTimer > 10)
            {
                anim.SetBool("Hitted", false);
                didGetTime = false;
                animatorTimer = 0;
                shotsTimer = 0;
                ResetScene();
                done = false;
            }
        }
        else if (state == AnimationState.DEATH)
        {
            if (!done)
            {
                aS.PlayOneShot(aC[2]);
            }
            anim.Play("DeadBoss");
            if (!didGetTime)
            {
                GetAnimTime();
                didGetTime = true;                
            }
            animatorTimer += Time.deltaTime;
            if (animatorTimer > animTime)
            {
                didGetTime = false;
                animatorTimer = 0;
                shotsTimer = 0;
                state = AnimationState.WIN;
            }
        }        
    }

    public void Shot()
    {
        shotsTimer += Time.deltaTime;
        if (shotsTimer > TimeBtwShots)
        {
            Instantiate(muzzle, spawn.position, Quaternion.identity);
            GameObject tempBullet = Instantiate(bullet, spawn.position, Quaternion.identity);
            tempBullet.GetComponent<BossBullet>().direction = -spawn.right;
            shotsTimer = 0;
        }        
    }

    public void GetDamagge()
    {
        if (CanGetDamage)
        {
            life--;
            CanGetDamage = false;
            anim.SetBool("Hitted", false);
            didGetTime = false;
            animatorTimer = 0;
            shotsTimer = 0;
            ResetScene();
            colorCounter = 0;
            state = AnimationState.IDLE;
        }
    }

    private void ResetScene()
    {
        ac.active = false;
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].SetBool("Active", true);
        }
        anim.SetBool("Hitted", false);
        if (redEnemyInGame == null)
            redEnemyInGame = Instantiate(redEnemy, redStartPosition, Quaternion.identity);
        for (int i = 0; i < panel.Length; i++)
        {
            panel[i].active = false;
        }
        ea.filled = false;
        ea.amountToFill = ea.startSize;
        box.position = startBoxPosition;
        state = AnimationState.IDLE;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            if (state == AnimationState.GETHIT)
            {
                colorCounter = 0;
                sr.color = Color.red;
                CanGetDamage = true;
            }
        }
    }
}
