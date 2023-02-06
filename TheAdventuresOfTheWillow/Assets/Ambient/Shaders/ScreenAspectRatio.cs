using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenAspectRatio : MonoBehaviour
{
	public bool m_isStarting;
	public GameObject m_target;
	public Image m_maskTransition;
    private RectTransform m_canvas;
	float m_screen_h = 0;
	float m_screen_w = 0;
	float m_radius = 0;
	public float target;
    public float smoothTime = 0.3f;
    public float yVelocity = 0.0f;
	public float timetoclose = 1f;
	public Camera cam;
	
	void Awake()
	{
		Open();
	}
    void Start()
    {
		m_canvas = GetComponent<RectTransform>();
		StartCoroutine(ToClose());
    }
	IEnumerator ToClose()
    {
		yield return new WaitForSeconds(timetoclose);
        Close();
    }
    public void Open()
	{
		m_isStarting = true;
		
	}	
	
	public void Close()
	{
		m_isStarting = false;
	}
    
    void Update()
    {
		if(m_isStarting)
		{
			target = 0;
		    m_radius =  Mathf.SmoothStep(m_radius, target, smoothTime);
		    m_maskTransition.material.SetFloat("Radius", m_radius);
		}
		else
		{
			target = 1;
		    m_radius =  Mathf.SmoothStep(m_radius, target, smoothTime);
		    m_maskTransition.material.SetFloat("Radius", m_radius);
		}
		
		Vector3 screenPos = cam.WorldToScreenPoint(m_target.transform.position);
		
		float characterScreen_w = 0;
		float characterScreen_h = 0;
		
		m_screen_h = Screen.height;
		m_screen_w = Screen.width;
		
		if(m_screen_w < m_screen_h) //Portrait
		{
			m_maskTransition.rectTransform.sizeDelta = new Vector2(m_canvas.rect.height, m_canvas.rect.height);
			float newScreenPos_x = screenPos.x + (m_screen_h - m_screen_w) / 2;
			
			characterScreen_w = (newScreenPos_x * 100) / m_screen_h;
			characterScreen_w /= 100;
			
			characterScreen_h = (screenPos.y * 100) / m_screen_h;
			characterScreen_h /= 100;
		}
        else //Landscape
	    {
			m_maskTransition.rectTransform.sizeDelta = new Vector2(m_canvas.rect.width, m_canvas.rect.width);
			float newScreenPos_y = screenPos.y + (m_screen_w - m_screen_h) / 2;
			
			characterScreen_w = (screenPos.x * 100) / m_screen_w;
			characterScreen_w /= 100;
			
			characterScreen_h = (newScreenPos_y * 100) / m_screen_w;
			characterScreen_h /= 100;
		}
		
		m_maskTransition.material.SetFloat("Center_X", characterScreen_w);
		m_maskTransition.material.SetFloat("Center_Y", characterScreen_h);
    }
	
}
