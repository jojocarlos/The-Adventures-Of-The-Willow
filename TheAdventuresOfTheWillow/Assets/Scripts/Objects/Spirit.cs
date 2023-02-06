using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spirit : MonoBehaviour
{
    public static Spirit spiritInstance;

    //target player
    private bool isFollowing;

    [SerializeField] private float _followSpeed = 1f;
    [SerializeField] private float _followBossSpeed = 2f;
    [SerializeField] private Transform _followTarget;

    public Transform followTarget;

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

    private bool attackpermited;

    private Animator Anim;

    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }
    }

    private void Start()
    {
        timetoattack = 2f;
        timetoagain = 1f;

        if(spiritInstance == null)
        {
            spiritInstance = this;
        }

        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(SpiderBoss.spiderBossInstance.isIdle && !isAttacking)
        {
            Anim.SetBool("ToAttack", true);
        }
        else
        {
            Anim.SetBool("ToAttack", false);
        }
    }

    private void FixedUpdate()
    {
        if (isFollowing && !isAttacking)
        {
            // including cos to target position.x and sin to target position.y
            Vector3 targetPosition = new Vector3(_followTarget.position.x + Mathf.Cos(Time.time * _frequency) * _amplitude,
                _followTarget.position.y + Mathf.Sin(Time.time * frequency) * amplitude,
                _followTarget.position.z);
            //now put above calculation into lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }

        if(isAttacking && SpiderBoss.spiderBossInstance.isIdle)
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
                if(!SpiderBoss.spiderBossInstance.isIdle)
                {
                    Anim.SetTrigger("Hurt");
                }
            }
        }
        if(timetoattack <= 0)
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
            if (!isFollowing)
            {
                KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
                followTarget = thePlayer.KeyFollowPoint;
                isFollowing = true;
                thePlayer.followingSpirit = this;
            }
        }
    }

}
