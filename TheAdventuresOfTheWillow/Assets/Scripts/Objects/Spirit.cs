using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spirit : MonoBehaviour, IDataPersistence
{
    public static Spirit spiritInstance;

    //target player
    public bool isSpiritFollowing;

    [SerializeField] private float _followSpeed = 1f;
    [SerializeField] private float _followBossSpeed = 2f;
    [SerializeField] private Transform _followTarget;

    //Sin & Con animation
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;

    [Header("For SeeingBoss")]
    [SerializeField] private Transform BossTarget;
    public bool isAttacking;
    private bool isAttacked;
    [SerializeField] private float frequencyAttack;
    [SerializeField] private float amplitudeAttack;
    [SerializeField] private float timetoattack = 2f;
    [SerializeField] private float timetoagain = 1f;


    [SerializeField] private Animator Anim;

    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }
    }
    private void Awake()
    {
        if (isSpiritFollowing)
        {
            isSpiritFollowing = true;
        }

    }

    private void Start()
    {
        timetoattack = 2f;
        timetoagain = 1f;

        if (spiritInstance == null)
        {
            spiritInstance = this;
        }
        KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
        _followTarget = thePlayer.KeyFollowPoint;
        thePlayer.followingSpirit = this;

    }

    private void Update()
    {
        if (!isAttacking)
        {
            Anim.SetBool("ToAttack", true);
        }
        else
        {
            Anim.SetBool("ToAttack", false);
        }
        KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
        _followTarget = thePlayer.KeyFollowPoint;
        thePlayer.followingSpirit = this;

        GameObject spiderBossObject = GameObject.FindWithTag("SpiderBoss");
        if (spiderBossObject == null)
        {
        }
        else
        {
            BossTarget = spiderBossObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (isSpiritFollowing && !isAttacking)
        {
            // including cos to target position.x and sin to target position.y
            Vector3 targetPosition = new Vector3(_followTarget.position.x + Mathf.Cos(Time.time * _frequency) * _amplitude,
                _followTarget.position.y + Mathf.Sin(Time.time * frequency) * amplitude,
                _followTarget.position.z);
            //now put above calculation into lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }

        if (isAttacking)
        {
            if (isAttacked)
            {
                Anim.SetBool("Attack", true);
                transform.position = Vector3.Lerp(transform.position, BossTarget.position, _followBossSpeed * Time.deltaTime);
                timetoattack -= 1 * Time.deltaTime;
            }
            if (!isAttacked)
            {
                Vector3 attackposition = new Vector3(BossTarget.position.x, BossTarget.position.y + Mathf.Sin(Time.time * frequencyAttack) * amplitudeAttack, BossTarget.position.z);
                transform.position = Vector3.Lerp(transform.position, attackposition, _followBossSpeed * Time.deltaTime);
                timetoagain -= 1 * Time.deltaTime;
                Anim.SetTrigger("Hurt");

            }
        }
        if (timetoattack <= 0)
        {
            timetoattack = 0;
            isAttacked = false;
        }
        if (timetoagain <= 0)
        {
            timetoagain = 0;
            StartCoroutine(ToAgain());
        }
    }

    IEnumerator ToAgain()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
        isAttacked = true;
        Anim.SetBool("Attack", false);
        timetoattack = 2f;
        timetoagain = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isSpiritFollowing)
            {
                KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
                _followTarget = thePlayer.KeyFollowPoint;
                isSpiritFollowing = true;
                thePlayer.followingSpirit = this;
            }
        }
    }

    public void LoadData(GameData data)
    {
        this.isSpiritFollowing = data.isSpiritFollowing;
    }

    public void SaveData(GameData data)
    {
        data.isSpiritFollowing = this.isSpiritFollowing;
    }
}