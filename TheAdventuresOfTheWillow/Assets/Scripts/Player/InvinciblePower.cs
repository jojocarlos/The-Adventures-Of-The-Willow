using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePower : MonoBehaviour
{
	public static InvinciblePower instance;

	public SpriteRenderer spriteRenderer;
	[SerializeField] [Range(0f, 10f)] float lerpTime;
	[SerializeField] Color[] myColors;
	[SerializeField] Color myNormalColor;
	
	int colorIndex = 0;
	float t = 0f;
	int len;
	
	public bool isInvincible = false;
	public float toStop = 10f;
	public PlayerHealth playerHealth;
	private bool MusicIsPlay;
    //music FMOD
    private EventInstance InvincibleMusic;

    void Start()
    {
		isInvincible = false;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		len = myColors.Length;
		if(instance == null)
		{
			instance = this;
		}
    }

    void Update()
    {
		if(isInvincible)
		{
			if(!MusicIsPlay)
			{
			    PlayMusic();
				MusicIsPlay = true;
			}
			playerHealth.isInvinciblePower = true;
            StartCoroutine(Invinciblestop());
            spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, myColors[colorIndex], lerpTime*Time.deltaTime);
		    t = Mathf.Lerp (t, 1f, lerpTime*Time.deltaTime);
		    if(t>.9f)
		    {
			    t = 0f;
			    colorIndex++;
			    colorIndex = (colorIndex >= len) ? 0 : colorIndex;
		    }
		}
		
		if(!isInvincible)
		{
			playerHealth.isInvinciblePower = false;
			spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, myNormalColor, lerpTime*Time.deltaTime);
		}
    }
    private void PlayMusic()
	{
        PLAYBACK_STATE playbackState;
        InvincibleMusic.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            InvincibleMusic.start();
        }
    }
    IEnumerator Invinciblestop()
    {
		yield return new WaitForSeconds(toStop);
		isInvincible = false;
        InvincibleMusic.stop(STOP_MODE.ALLOWFADEOUT);
        MusicIsPlay = false;
    }
}
