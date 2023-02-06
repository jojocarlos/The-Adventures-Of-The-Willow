using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thunder : MonoBehaviour
{
	public Image thunderBrightnessImage;
	public float thunderTimer;
	private Color newColor;
	private float brightColor;
	public UnityEngine.Rendering.Universal.Light2D thunderLight;
	
	private float timer;
	bool isThunder;
	
	public AudioSource thunderSound;
	
    void Start()
    {
		newColor = thunderBrightnessImage.color;
		newColor.a = 0;
		thunderBrightnessImage.color = newColor;
        timer = 1;
    }

    
    void Update()
    {
        thunderTimer -= Time.deltaTime;
		
		if(isThunder)
		{
			timer -= Time.deltaTime * 2;
			thunderLight.intensity = 5f;
			brightColor = timer;
			newColor.a = brightColor;
			thunderBrightnessImage.color = newColor;
			
			if(brightColor <= 0)
			{
				thunderLight.intensity = 0.4f;
				thunderTimer = Random.Range(10, 20);
				isThunder = false;
			}
			return;
		}
		
		if(thunderTimer <= 0)
		{
			isThunder = true;
			timer = 1;
			brightColor = timer;
			
			thunderSound.Play();
		}
    }
}
