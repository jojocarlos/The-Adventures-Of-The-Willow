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

public class SpiderBoss : MonoBehaviour, IDataPersistence
{
    public static SpiderBoss spiderBossInstance;

    private SpiderBossStatus status;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject SpiderObject;
    [SerializeField] private float statusCh;

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
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private float TimerDamage = 0;
    [SerializeField] private float TimeDamage = 1f;
    [SerializeField] private bool canDamage = false;

    public bool isIdle;
    private bool flipRight;
    public bool spiderBossDefeat;
    private GameObject colliderTrigger;
    public bool SpiderFinalStarted;
    [SerializeField] private Collider2D[] SpiderColliders;

    //EarthQuake
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private ScreenShakeProfile profile;
    [SerializeField] private FallingPlatformSpiderBoss fallingPlatformSpiderBoss;
    [SerializeField] private FallingObjectsSpiderBoss fallingObjectsSpiderBoss;


    //Shoot Web
    [SerializeField] private GameObject webPrefab;
    [SerializeField] private float webSpeed = 10f;
    [SerializeField] private Transform webSpawnPoint;
    [SerializeField] private string targetTag = "Player";

    [SerializeField] private PlayerBossArea playerBossArea;

    void Start()
    {
        colliderTrigger = GameObject.Find("SpiderBossEnterCollider");
        currentHealth = maxHealth;
        if (spiderBossInstance == null)
        {
            spiderBossInstance = this;
        }
        if(spiderBossDefeat)
        {
            SpiderObject.SetActive(false);
            colliderTrigger.SetActive(false);
            BossesHealth.BossesHealthInstance.isSpider = false;
        }
        else
        {
            SpiderObject.SetActive(true);
        }
        anim.SetBool("StartNow", true);
        StopAllCoroutines();
    }
    public void BossInitial() 
    {
        anim.SetBool("StartNow", false);
        status = SpiderBossStatus.WALK;
        currentTime = secondsWalk;
        StopAllCoroutines();
    }
    public void BossStart()
    {
        if (!spiderBossDefeat)
        {
            BossesHealth.BossesHealthInstance.isSpider = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX2, this.transform.position);
            Angry = true;
            currentHealth = maxHealth;
            BossesHealth.BossesHealthInstance.SetMaxHealthSpider(maxHealth);
        }
    }


    private void Update()
    {
        if(!playerBossArea.playerInArea)
        {
            StopAllCoroutines();
            anim.SetBool("StartNow", true);
        }
        AudioManager.instance.SetFightMusicArea("SpiderHealth", currentHealth);
        if (canDamage)
        {
            TimerDamage -= Time.deltaTime;
            if (TimerDamage <= 0)
            {
                canDamage = false;
            }
        }

        if (currentHealth <= 0)
        {
            spiderBossDefeat = true;
        }
        if(spiderBossDefeat && !SpiderFinalStarted)
        {
            foreach (Collider2D other in SpiderColliders)
            {
                other.enabled = false;
            }
            InitializeCutscenes.initializeCutscenesInstance.FinalSceneSpider();
            anim.SetBool("Facing", false);
            SpiderFinalStarted = true;
            StopAllCoroutines();
            BossesHealth.BossesHealthInstance.isSpider = false;
            StartCoroutine(ToDesapear());
        }
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

    void TakeDamage(int damage)
    {
        BossesHealth.BossesHealthInstance.SetHealthSpider(currentHealth);
		if (!canDamage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            else
            {
                canDamage = true;
                TimerDamage = TimeDamage;
            }
        }
    }
	
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag ("Spirit"))
        {
            //When is idle and player attack
            if (isIdle && Spirit.spiritInstance.isAttacking)
            {
                TakeDamage(5);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX7, this.transform.position);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement2D.PlayerMovement2Dinstance.KnockBackCount = PlayerMovement2D.PlayerMovement2Dinstance.KnockBackLength;
            if (col.transform.position.x < transform.position.x)
            {
                PlayerMovement2D.PlayerMovement2Dinstance.KnockFromRight = true;
            }
            else
            {
                PlayerMovement2D.PlayerMovement2Dinstance.KnockFromRight = false;
            }
        }
    }
   

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
    IEnumerator ToDesapear()
    {
        yield return new WaitForSeconds(10f);
        anim.SetTrigger("isDefeat");
        if(!flipRight)
        {
            Quaternion newRotation = transform.localRotation;
            newRotation *= Quaternion.Euler(0, 180, 0);
            transform.localRotation = newRotation;
        }

        yield return new WaitForSeconds(5);
        anim.SetBool("SpiderTalk", true);

        yield return new WaitForSeconds (40f);
        SpiderObject.SetActive(false);
        colliderTrigger.SetActive(false);
    }
    IEnumerator Idle()
    {
        statusCh = 4;
        yield return new WaitForSeconds(IdleTime);
    }
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
    IEnumerator AngryStatus()
    {
        statusCh = 3;
        yield return new WaitForSeconds(AngryTime);
    }

    //SFX
    public void AngrySFX()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX1, this.transform.position);
    }
    public void AttackSFX()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX3, this.transform.position);
    }
    public void Attack02SFX()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX4, this.transform.position);
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX5, this.transform.position);
        fallingPlatformSpiderBoss.FallNow();
        fallingObjectsSpiderBoss.SpawnFallNow();
    }
    public void shakeNowJump()
    {
        //CameraShake.CameraShakeinstance.camerashake(impulseSource);
        CameraShake.CameraShakeinstance.ScreenShakeFromProfile(profile, impulseSource);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX6, this.transform.position);
        fallingPlatformSpiderBoss.FallNow();
        fallingObjectsSpiderBoss.SpawnFallNow();
    }
    public void FlifLeft()
    {
        if (flipRight)
        {
            flipRight = false;
            anim.SetBool("Facing", false);
            Quaternion newRotation = transform.localRotation;
            newRotation *= Quaternion.Euler(0, 180, 0);
            transform.localRotation = newRotation;
        }
    }
    public void FlipRight()
    {
        if (!flipRight)
        {
            flipRight = true;
            anim.SetBool("Facing", true);
            Quaternion newRotation = transform.localRotation;
            newRotation *= Quaternion.Euler(0, -180, 0);
            transform.localRotation = newRotation;
        }
    }


    //ShootWeb
    public void Shoot()
    {
        GameObject web = Instantiate(webPrefab, webSpawnPoint.position, Quaternion.identity) as GameObject;

        // Find the target object
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);

        if (target != null)
        {
            // Calculate the direction to the target
            Vector2 direction = (target.transform.position - webSpawnPoint.position).normalized;

            // Set the bullet's velocity to the calculated direction times the bullet speed
            web.GetComponent<Rigidbody2D>().velocity = direction * webSpeed;
        }
    }
    public void Talk()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Talk, this.transform.position);
    }

    public void LoadData(GameData data)
    {
        this.spiderBossDefeat = data.spiderBossDefeat;
        this.SpiderFinalStarted = data.SpiderFinalStarted;
    }

    public void SaveData(GameData data)
    {
        data.spiderBossDefeat = this.spiderBossDefeat;
        data.SpiderFinalStarted = this.SpiderFinalStarted;
    }
}