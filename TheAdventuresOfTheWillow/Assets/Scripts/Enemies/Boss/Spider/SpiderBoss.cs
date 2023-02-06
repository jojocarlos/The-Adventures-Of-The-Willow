using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Cinemachine;


public enum SpiderBossStatus
{ 
    WALK,
    Angry,
    IDLE,
    JUMP,
    ATTACKONE,
    ATTACKTWO,
    DEAD
}

public class SpiderBoss : MonoBehaviour
{
    public static SpiderBoss spiderBossInstance;

    public SpiderBossStatus status;
    Animator anim;
    public float statusCh;

    [Header("Walk Status")]
    [SerializeField] private float currentTime;
    [SerializeField] private float secondsWalk = 5f;


    [Header("Angry Status")]
    private bool Angry;
    [SerializeField] float AngryTime;

    [Header("Attack Status")]
    //AttackTwo
    [SerializeField] private float AttackTwoTime;

    [Header("Jump Status")]
    [SerializeField] private float JumpTimeWait;
    [SerializeField] private float IdleTime;

    [Header("Spider Health")]
    public float currenthealth = 100f;
    private float health;
    public float damage;
    private float timeBtwDamage = 1.5f;
    public Slider healthBar;
    public bool isIdle;

    private bool facingRight;

    //SFX
    public SpiderSFX spidersfx;

    //EarthQuake
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private ScreenShakeProfile profile;

    void Start()
    {
        status = SpiderBossStatus.WALK;
        spidersfx.SFX2.Play();
        currentTime = secondsWalk;
        Angry = true;
        anim = GetComponent<Animator>();
        health = currenthealth;
        healthBar.value = health;

        if (spiderBossInstance == null)
        {
            spiderBossInstance = this;
        }
    }


    private void Update()
    {
        healthBar.value = health;
    }

    private void FixedUpdate()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime >= 0)
        {
            anim.SetBool("isWalk", true);
        }
        if (currentTime <= 0)
        {
            anim.SetBool("isWalk", false);
            currentTime = 0;
        }
        if (currentTime <= 0 && Angry)
        {
            anim.SetTrigger("Angry");
            Angry = false;
            StartCoroutine(SpiderBossStatuses());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spirit")
        {
            //When is idle and player attack
            if (isIdle && Spirit.spiritInstance.isAttacking)
            {
                if (timeBtwDamage >= 0)
                {
                    TakeDamage(5);
                    timeBtwDamage -= Time.deltaTime;
                }
            }
        }
    }

    /*/Not used
     * Heal(5);
    public void Heal(float healingAmount)
    {
        health += healingAmount;
        health = Mathf.Clamp(healingAmount, 0, 100);

    }*/

    private float randomAttack;
    IEnumerator SpiderBossStatuses()
    {
        var r = Random.Range(0, 100);

        if (r < 20)
        {
            randomAttack = 1;
        }
        else if (r >= 20 && r < 40)
        {
            randomAttack = Random.Range(2, 3); ;
        }
        else if (r >= 40)
        {
            randomAttack = Random.Range(3, 6);
        }
        yield return new WaitForSeconds(statusCh);

        switch(randomAttack)
        {
            case 1:
                status = SpiderBossStatus.IDLE;
                break;
            case 2:
                status = SpiderBossStatus.JUMP;
                break;
            case 3:
                status = SpiderBossStatus.ATTACKTWO;
                break;
            case 4:
                status = SpiderBossStatus.ATTACKONE;
                break;
            case 5:
                status = SpiderBossStatus.Angry;
                break;
            default:
                break;
        }
        StatusChanger();
    }

    public void StatusChanger()
    {
        switch (status)
        {
            case SpiderBossStatus.IDLE:
                isIdle = true;
                StartCoroutine(Idle());
                StartCoroutine(SpiderBossStatuses());
                break;
            case SpiderBossStatus.JUMP:
                anim.SetTrigger("Jump");
                StartCoroutine(Jump());
                StartCoroutine(SpiderBossStatuses());
                break;
            case SpiderBossStatus.ATTACKONE:
                anim.SetTrigger("Attack");
                StartCoroutine(SpiderBossStatuses());
                break;
            case SpiderBossStatus.ATTACKTWO:
                anim.SetTrigger("Attack2");
                StartCoroutine(AttackTwo());
                StartCoroutine(SpiderBossStatuses());
                break;
            case SpiderBossStatus.Angry:
                anim.SetTrigger("Angry");
                StartCoroutine(AngryStatus());
                StartCoroutine(SpiderBossStatuses());
                break;
            default:
                break;
        }
    }

    IEnumerator Idle()
    {
        statusCh = 4;
        yield return new WaitForSeconds(IdleTime);
    }
    //Shoot Webs
    IEnumerator AttackTwo()
    {
        statusCh = 3;
        yield return new WaitForSeconds(AttackTwoTime);
    }

    IEnumerator Jump()
    {
        statusCh = 8;
        yield return new WaitForSeconds(JumpTimeWait);
    }

    void FlipRight()
    {
        facingRight = true;
        anim.SetBool("Facing", true);
        transform.Rotate(0, 180, 0);
    }
    void FlipLeft()
    {
        facingRight = false;
        anim.SetBool("Facing", false);
        transform.Rotate(0, -180, 0);
    }

    IEnumerator AngryStatus()
    {
        statusCh = 3;
        yield return new WaitForSeconds(AngryTime);
    }

    //SFX
    public void AngrySFX()
    {
        spidersfx.SFX1.Play();
    }
    public void AttackSFX()
    {
        spidersfx.SFX3.Play();
    }
    public void Attack02SFX()
    {
        spidersfx.SFX4.Play();
    }

    //all states go to idle, evit bug to attack when is idle out of idle state. for player attack.
    public void OnIdle()
    {
        isIdle = true;
    }
    public void OnIdleFalse()
    {
        isIdle = false;
    }

    public void shakeNow()
    {
        //CameraShake.CameraShakeinstance.camerashake(impulseSource);
        CameraShake.CameraShakeinstance.ScreenShakeFromProfile(profile, impulseSource);
        spidersfx.SFX5.Play();
    }
    public void shakeNowJump()
    {
        //CameraShake.CameraShakeinstance.camerashake(impulseSource);
        CameraShake.CameraShakeinstance.ScreenShakeFromProfile(profile, impulseSource);
        spidersfx.SFX6.Play();
    }

}