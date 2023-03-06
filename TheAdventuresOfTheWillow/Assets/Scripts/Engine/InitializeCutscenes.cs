using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InitializeCutscenes : MonoBehaviour
{
    //For initialize another scenes

    public static InitializeCutscenes initializeCutscenesInstance;

    [SerializeField] private PlayableDirector SpiderFinalScene;


    private void Start()
    {
        if(initializeCutscenesInstance == null)
        {
            initializeCutscenesInstance = this;
        }
    }

    public void FinalSceneSpider()
    { 
        SpiderFinalScene.Play();
    }
}
