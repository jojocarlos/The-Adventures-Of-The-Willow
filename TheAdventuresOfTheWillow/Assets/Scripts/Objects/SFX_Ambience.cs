using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Ambience : MonoBehaviour
{
    public AudioSource sfxaudio;
	
	public void startAudio()
	{
		sfxaudio.Play();
	}
	
	public void stopAudio()
	{
		sfxaudio.Stop();
	}
}
