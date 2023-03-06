using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponentsForCutScenes : MonoBehaviour
{
    [SerializeField] private Transform SpiderFinalPosition;
    [SerializeField] private float moveSpeed = 1f;

    //StartCutscences stop player controls
    public void cutSceneStart()
    {
        //Instance Player when cutscene is started
        PlayerMovement2D.PlayerMovement2Dinstance.CutsceneStateTrue();
    }
    public void cutSceneStop()
    {
        //Instance Player when cutscene is started
        PlayerMovement2D.PlayerMovement2Dinstance.CutsceneStateFalse();
    }
    //move player to final spider position to another cutscene
    private void MovePlayerToPointSpiderFinalPosition()
    {
        StartCoroutine(MoveToDestination());
    }
    IEnumerator MoveToDestination()
    {
        yield return new WaitForSeconds(0.5f);
        float destinationPositionX = SpiderFinalPosition.position.x;
        Vector3 newPosition = PlayerMovement2D.PlayerMovement2Dinstance.transform.position;
        newPosition.x = destinationPositionX;

        while (Mathf.Abs(PlayerMovement2D.PlayerMovement2Dinstance.transform.position.x - destinationPositionX) > 0.1f)
        {
            float newXPosition = Mathf.MoveTowards(PlayerMovement2D.PlayerMovement2Dinstance.transform.position.x, destinationPositionX, moveSpeed * Time.deltaTime);
            newPosition.x = newXPosition;
            PlayerMovement2D.PlayerMovement2Dinstance.transform.position = newPosition;
            PlayerAnimations.PlayerAnimationsInstance.animationPlayer.SetBool("IsWalking", true);
            if(!PlayerMovement2D.PlayerMovement2Dinstance.facingRight)
            {
                PlayerMovement2D.PlayerMovement2Dinstance.Turn();
            }
            yield return null;
        }
        PlayerAnimations.PlayerAnimationsInstance.animationPlayer.SetBool("IsWalking", false);
    }

    #region Signals
    //signals for call Boss initial fuctions

    //SpiderBossSignal

    public void SpiderSceneInitial()
    {
        SpiderBoss.spiderBossInstance.BossInitial();
    }
    public void SpiderSceneStart()
    {
        SpiderBoss.spiderBossInstance.BossStart();
    }

    //WerewolfBossSignal
    public void WerewolfSceneInitial()
    {
        WerewolfBossAttack1.werewolfBossAttack1Instance.BossInitial();
    }
    //add more signals
    #endregion
}
