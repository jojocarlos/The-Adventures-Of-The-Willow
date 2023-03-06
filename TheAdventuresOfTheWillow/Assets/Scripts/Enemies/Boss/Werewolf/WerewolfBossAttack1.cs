using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfBossAttack1 : MonoBehaviour
{
    public static WerewolfBossAttack1 werewolfBossAttack1Instance;

    [SerializeField] private Animator anim;
    [SerializeField] private Animator fallTorchAnim;
    void Start()
    {
        if(werewolfBossAttack1Instance == null)
        {
            werewolfBossAttack1Instance = this;
        }
    }

    public void BossInitial()
    {
        anim.SetTrigger("Action");
    }
    public void InitiateFallTorch()
    {
        fallTorchAnim.SetBool("FallTorch", true);
    }
    void Update()
    {
        
    }
}
