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

    void Start()
    {
        startBoxPosition = box.position;
        redStartPosition = redEnemyInGame.transform.position; ;
        state = AnimationState.IDLE;
        shotsTimer = 0;
        anim = GetComponent<Animator>();
    }

    void GetAnimTime()
    {
        AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        animTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
        
    }

    void Update()
    {
        if (ac.active)
        {
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
            counter += Time.deltaTime;
            if (counter > timeToAttack)
            {
                state = AnimationState.STEP;
                counter = 0;
            }
        }
        else if (state == AnimationState.STEP)
        {
            anim.SetBool("Step", true);
            if (!didGetTime)
            {
                GetAnimTime();
                if (animTime > 5)
                {
                    animTime = 5;
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
            anim.SetBool("Step", false);
            if (!didGetTime)
            {
                GetAnimTime();
                didGetTime = true;
            }
            Shot();
            animatorTimer += Time.deltaTime;
            if (animatorTimer > animTime)
            {
                anim.SetBool("Attaking", false);
                state = AnimationState.IDLE;
                didGetTime = false;
                animatorTimer = 0;
                shotsTimer = 0;
            }
        }
        else if (state == AnimationState.GETHIT)
        {
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
            }
        }
        else if (state == AnimationState.DEATH)
        {
            anim.Play("DeadBoss");
            if (!didGetTime)
            {
                GetAnimTime();
                didGetTime = true;                
            }
            animatorTimer += Time.deltaTime;
            if (animatorTimer > animTime)
            {
                state = AnimationState.WIN;
            }
        }
        else if (state == AnimationState.WIN)
        {
            winCanvas.SetActive(true);
        }
        else if (state == AnimationState.LOSE)
        {
            loseCanvas.SetActive(true);
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
        box.position = startBoxPosition;
        state = AnimationState.IDLE;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            CanGetDamage = true;            
        }
    }
}
