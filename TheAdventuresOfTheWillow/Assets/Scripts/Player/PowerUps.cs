using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class PowerUps : MonoBehaviour
{
    private int collectedStars, victoryCondition = 3;
    private static PowerUps instance;

    public LevelButton levelButton;
    private GameObject Player;
    private PlayerMovement2D playerMovement2D;
    public Finish _finish;

    private GameObject FinishPoleObj;
    private Animator anim;
    public GameObject Block;
	
    public AudioSource audioplay;
    public AudioSource audioGameStop;
	

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public static PowerUps MyInstance
    {
        get
        {
            if (instance == null)
            instance = new PowerUps();

            return instance;
        }
    }
   
    private void Start() 
    {
        UIPowerUps.MyInstance.updateStarUI(collectedStars, victoryCondition);
        Player = GameObject.FindGameObjectWithTag("Player");
        playerMovement2D = Player.GetComponent<PlayerMovement2D>();
        FinishPoleObj = GameObject.FindGameObjectWithTag("Win");
        anim = FinishPoleObj.GetComponent<Animator>();
    }

    public void AddStars(int _stars)
    {
        collectedStars += _stars;
        UIPowerUps.MyInstance.updateStarUI(collectedStars, victoryCondition);
    }

    public void Finish()
    {
        if (collectedStars >= victoryCondition)
        {
            FinishSequence();
            //playerMovement2D.isWinning = true;
            _finish.PlayFireworksEffect();
            anim.SetBool("Winner", true);
			Block.SetActive(false);
			audioplay.Play();
			audioGameStop.Stop();
			DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            UIPowerUps.MyInstance.ShowVictoryCondition(collectedStars, victoryCondition);
			Block.SetActive(true);
        }

    }

    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(10);
        levelButton.LevelFinhished();
        _finish.StopFireworksEffect();
    }

    public void FinishSequence()
    {
        //playerMovement2D.walkwin();
        StartCoroutine(WinSequence());
    }
}
