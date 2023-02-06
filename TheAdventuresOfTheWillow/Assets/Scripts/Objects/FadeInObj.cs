using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInObj : MonoBehaviour
{
    public float fadeDelay = 10f;
	public float alphaValue = 0;
	public bool destroyGameObject = false;
	private bool isfade = false;
	
	public UnityEngine.U2D.SpriteShapeRenderer spriteRenderer;
	
	private void OnTriggerEnter2D(Collider2D col)
    {
        if(!isfade && col.gameObject.tag == "Player")
		{
            StartCoroutine(FadeTo(alphaValue, fadeDelay));
			isfade = true;
		}
	}

    private void OnTriggerStay2D(Collider2D col)
    {
        if(!isfade && col.gameObject.tag == "Player")
		{
            StartCoroutine(FadeTo(alphaValue, fadeDelay));
			isfade = true;
		}
	}
	
	IEnumerator FadeTo(float aValue, float fadeTime)
	{
		float alpha = spriteRenderer.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha, aValue, t));
			yield return null;
		}
		if(destroyGameObject)
			gameObject.SetActive(false);
	}
	
}
