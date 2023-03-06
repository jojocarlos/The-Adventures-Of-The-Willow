using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossesHealth : MonoBehaviour
{
    public static BossesHealth BossesHealthInstance;
    //Spider
    [SerializeField] private Slider sliderSpider;
    [SerializeField] private GameObject GOSliderSpider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [HideInInspector] public bool isSpider;

    private void Start()
    {
        if(BossesHealthInstance == null)
        {
            BossesHealthInstance = this;
        }
    }
    private void Update()
    {
        if(isSpider)
        {
            GOSliderSpider.SetActive(true);
        }
        else
        {
            GOSliderSpider.SetActive(false);
        }
    }
    public void SetMaxHealthSpider(int health)
    {
        sliderSpider.maxValue = health;
        sliderSpider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealthSpider(int health)
    {
        sliderSpider.value = health;
        fill.color = gradient.Evaluate(sliderSpider.normalizedValue);
    }

}
