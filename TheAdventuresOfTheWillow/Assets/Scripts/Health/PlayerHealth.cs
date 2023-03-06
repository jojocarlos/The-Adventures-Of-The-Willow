using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD.Studio;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public static PlayerHealth PlayerHealthInstance;

    //Health
    public int maxHealth = 100;
    public int currentHealth;
    public Health healthbar;
    private float myTimeDamage = 0;


    //Count Dead
    public DeathCountInfo DCI;

    //PlayerStates
    public SpriteRenderer playerColor;
    public bool isExplode;
    public Powers powers;
    public bool isInvinciblePower = false;
    private PlayerAnimations playerAnimations;

    private bool canDamage;
    public float toDie = 1f;

    //Pos
    public Vector3 playerPosInWorld1;
    public Vector3 playerPosInWorld2;
    public bool isWorld1;
    public bool isWorld2;


    void Start()
    {
        if(PlayerHealthInstance == null)
        {
            PlayerHealthInstance = this;
        }
       

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        playerAnimations = GetComponent <PlayerAnimations>();

        DataPersistenceManager.instance.LoadGame();
        if(isWorld1)
        {
            transform.position = playerPosInWorld1;
        }
        if (isWorld2)
        {
            transform.position = playerPosInWorld2;
        }
    }

    void Update()
    {
        //TakeDamage(20);
        Damage();

        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

    }

    public void AddHealth(int addhealth)
    {
        currentHealth += addhealth;

        healthbar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }
    void Damage()
    {
        if (canDamage == false)
        {
            DamageTimer();
        }
    }

    void DamageTimer()
    {
        myTimeDamage += Time.deltaTime;
        if (myTimeDamage > 0.5f)
        {
            canDamage = true;
            myTimeDamage = 0;
            playerColor.color = UnityEngine.Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (canDamage && isExplode == false && powers.isPowered == false && !isInvinciblePower)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Hurt);
                TakeDamage(5);
                canDamage = false;
                playerColor.color = UnityEngine.Color.red;
                //can die if currentHealth is <=0
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
            if (canDamage && !isExplode && powers.isPowered)
            {
                powers.isNormalState();
            }
        }
        //Checkpoint and get checkpoint positions
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            playerAnimations.PlayerStart();
            //initialPos = col.gameObject.transform.position;
            if(isWorld1)
            {
                playerPosInWorld1 = col.gameObject.transform.position; 
            }
            if (isWorld2)
            {
                playerPosInWorld2 = col.gameObject.transform.position;
            }
            DataPersistenceManager.instance.SaveGame();
            col.GetComponent<Animator>().SetTrigger("appear");
        }

        //imediate dead enemies
        if (col.gameObject.tag == "morte_imediata")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead laser
        if(col.gameObject.tag == "Laser")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead if outside of Game

        if (col.gameObject.tag == "morte_imediata_Chao")
        {
            if (canDamage)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
        }

    }

    //Dead
    void Die()
    {
        //só reinicia quando acaba as chances
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            canDamage = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Dead);
            playerAnimations.PlayerDie();
            PlayerMovement2D.PlayerMovement2Dinstance.isDead = true;
            StartCoroutine(RestartDie());
            DataPersistenceManager.instance.SaveGame();
        }
    }

    void Restart()
    {
        canDamage = false;
        //mudo a posiçao do personagem
        if(isWorld1)
        {
            transform.position = playerPosInWorld1;
        }
        if (isWorld2)
        {
            transform.position = playerPosInWorld2;
        }
        //recuperar vida
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        playerAnimations.PlayerStart();
        PlayerMovement2D.PlayerMovement2Dinstance.isDead = false;
        DCI.DeadOneMore();
    }


    IEnumerator RestartDie()
    {
        yield return new WaitForSeconds(toDie);
        Restart();
    }



    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
        this.isWorld1 = data.isWorld1;
        this.isWorld2 = data.isWorld2;
        this.playerPosInWorld1 = data.playerPosInWorld1;
        this.playerPosInWorld2 = data.playerPosInWorld2;
    }

    public void SaveData(GameData data)
    {
        data.currentHealth = this.currentHealth;
        data.isWorld1 = this.isWorld1;
        data.isWorld2 = this.isWorld2;
        data.playerPosInWorld1 = this.playerPosInWorld1;
        data.playerPosInWorld2 = this.playerPosInWorld2;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (canDamage && isExplode == false && powers.isPowered == false && !isInvinciblePower)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Hurt);
                TakeDamage(5);
                canDamage = false;
                playerColor.color = UnityEngine.Color.red;
                //can die if currentHealth is <=0
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
            if (canDamage && !isExplode && powers.isPowered)
            {
                powers.isNormalState();
            }
        }

        //imediate dead enemies
        if (col.gameObject.tag == "morte_imediata")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead laser
        if (col.gameObject.tag == "Laser")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead if outside of Game

        if (col.gameObject.tag == "morte_imediata_Chao")
        {
            if (canDamage)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (canDamage && isExplode == false && powers.isPowered == false && !isInvinciblePower)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Hurt);
                TakeDamage(5);
                canDamage = false;
                playerColor.color = UnityEngine.Color.red;
                //can die if currentHealth is <=0
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
            if (canDamage && !isExplode && powers.isPowered)
            {
                powers.isNormalState();
            }
        }

        //imediate dead enemies
        if (col.gameObject.tag == "morte_imediata")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead laser
        if (col.gameObject.tag == "Laser")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead if outside of Game

        if (col.gameObject.tag == "morte_imediata_Chao")
        {
            if (canDamage)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            if (canDamage && isExplode == false && powers.isPowered == false && !isInvinciblePower)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Hurt);
                TakeDamage(5);
                canDamage = false;
                playerColor.color = UnityEngine.Color.red;
                //can die if currentHealth is <=0
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
            if (canDamage && !isExplode && powers.isPowered)
            {
                powers.isNormalState();
            }
        }
        //imediate dead enemies
        if (col.gameObject.tag == "morte_imediata")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead laser
        if (col.gameObject.tag == "Laser")
        {
            if (canDamage && !isExplode && !powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
            if (canDamage && !isExplode && powers.isPowered && !isInvinciblePower)
            {
                canDamage = false;
                powers.isNormalState();
            }
        }

        //dead if outside of Game

        if (col.gameObject.tag == "morte_imediata_Chao")
        {
            if (canDamage)
            {
                canDamage = false;
                //remove all health and die
                TakeDamage(maxHealth);
                Die();
            }
        }

    }

}
