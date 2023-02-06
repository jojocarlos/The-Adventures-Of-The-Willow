using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Animator Anim;
	private bool isopened;
	private ParticleSystem particles;
	
	[SerializeField] private float toOpen = 2f;
	[SerializeField] private float toIdle = 2f;
	
    void Start()
    {
        Anim = GetComponent<Animator>();
		particles = GetComponentInChildren<ParticleSystem>();
		isopened = false;
    }

    
    void Update()
    {
		if(!isopened)
		{
            StartCoroutine(Close());
            Anim.SetBool("Open", false);
			isopened = true;
		}
    }
	
	IEnumerator Close()
	{
		yield return new WaitForSeconds(toOpen);
        Anim.SetBool("Open", true);
        StartCoroutine(Idle());
	}
	IEnumerator Idle()
	{
		yield return new WaitForSeconds(toIdle);
        Anim.SetTrigger("Idle");
        isopened = false;
	}
	
	public void playParticles()
	{
		particles.Play();
	}
	
}
