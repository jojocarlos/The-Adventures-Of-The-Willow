using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPowerUps : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TXTStars, TXTWinCondition;
    [SerializeField] GameObject victoryCondition;

    private static UIPowerUps instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate (this);
        }
    }

    public static UIPowerUps MyInstance
    {
        get
        {
            if (instance == null)
            instance = new UIPowerUps();

            return instance;
        }
    }

    public void updateStarUI(int _stars, int _victoryCondition)
    {
        TXTStars.text = _stars + " / " + _victoryCondition;
    }

    public void ShowVictoryCondition(int _stars, int _victoryCondition)
    {
        victoryCondition.SetActive(true);
        TXTWinCondition.text = "You need " + (_victoryCondition - _stars) + " more Stars";
    }

    public void HideVictoryCondition()
    {
        victoryCondition.SetActive(false);
    }
}
