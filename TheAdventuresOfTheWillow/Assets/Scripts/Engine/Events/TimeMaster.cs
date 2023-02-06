using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeMaster : MonoBehaviour
{
    public TextMeshProUGUI Time;
    public float msToWait;
    private ulong lastSeasonTime;
	//public bool IsCountDown;
	public bool IsTimeFinished;
	public bool itsCounting;
     
    private void Start()
    {
     
        lastSeasonTime = ulong.Parse(PlayerPrefs.GetString("LastSeasonTime"));
		
    }
	
    public void CountingDownStart()
	{
		
		if(!itsCounting)
		{
		    lastSeasonTime = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString("LastSeasonTimeClicked", lastSeasonTime.ToString());
			itsCounting = true;
			IsTimeFinished = false;
		}
	}
	public void Click()
    {
        lastSeasonTime = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastSeasonTimeClicked", lastSeasonTime.ToString());
    }
    private void Update()
    {
		int value;
		value = itsCounting ? 1 : 0;
		if(itsCounting == true)
		{
			value = 1;
		}
		else
		{
			value = 0;
		}
		
		Debug.Log("update");
        if (!IsTimeFinished)
        {
            if (Ready())
            {
                Time.text = "Ready!";
                return;
            }
            ulong diff = ((ulong)DateTime.Now.Ticks - lastSeasonTime);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;
     
            string r = "";
            //Months
			r += ((int)secondsLeft / 2630000).ToString() + "m";
            secondsLeft -= ((int)secondsLeft / 2630000) * 2630000;
			//Weekends
			r += ((int)secondsLeft / 604800).ToString() + "w";
            secondsLeft -= ((int)secondsLeft / 604800) * 604800;
			//Days
			r += ((int)secondsLeft / 86400).ToString() + "d";
            secondsLeft -= ((int)secondsLeft / 86400) * 86400;
            //HOURS 3600seconds = 1hour
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //MINUTES 60seconds = 1 minute
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //SECONDS
            r += (secondsLeft % 60).ToString("00") + "s";
            Time.text = r;
        }
    }
     
    
    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastSeasonTime);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
     
        float secondsLeft = (float)(msToWait - m) / 1000.0f;
     
        if (secondsLeft < 0)
        {
            //DO SOMETHING WHEN TIMER IS FINISHED
			IsTimeFinished = true;
            return true;
        }
     
            return false;
    }
	
}

