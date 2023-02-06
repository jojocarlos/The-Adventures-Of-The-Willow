using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class Timer2 : MonoBehaviour
{
    [SerializeField] DateTime currentDate;
    [SerializeField] DateTime startDate;
    [SerializeField] DateTime endDate;
    public TMP_Text FinishTimeText;
	public bool timer;
	
    void Start()
    {
        /*/ Setting timer start and end dates
 
        currentDate = DateTime.Now; // Checking current date, time etc
        Debug.Log(currentDate); // Checking if it actually shows current date
       
        startDate = new DateTime(2022, 12, 10, 18, 0, 0); // Set the start date
        Debug.Log(startDate); // Checking if it actually sets the start date

        endDate = new DateTime(2022, 12, 25, 18, 0, 0); // Set the end date
        Debug.Log(endDate); // Checking if it actually sets the end date
        
		TimeSpan timeDifference = currentDate - endDate; // Seeing what is the time difference
		
        //string time = new DateTime(timeDifference.Ticks).ToString("dd:hh:mm:ss");
		//string time = timeDifference.ToString("dd:hh:mm:ss");
		string time = timeDifference.ToString("dd\\:hh\\:mm\\:ss");
        Debug.Log(time);
		
        FinishTimeText.text = time;
		
		if(currentDate >= endDate)
        {
            Debug.Log("Event has ended");
            FinishTimeText.text = "Event Ended";
        }
		*/
    }
		
	void Update()
    {
		// Setting timer start and end dates
 
        currentDate = DateTime.Now; // Checking current date, time etc
       
        startDate = new DateTime(2022, 12, 10, 18, 0, 0); // Set the start date

        endDate = new DateTime(2022, 12, 24, 18, 0, 0); // Set the end date
		        
		TimeSpan timeDifference = currentDate - endDate; // Seeing what is the time difference
		
		string time = timeDifference.ToString("dd\\:hh\\:mm\\:ss");
		
        FinishTimeText.text = time;
		
		if(currentDate >= endDate)
        {
            Debug.Log("Event has ended");
            FinishTimeText.text = "Event Ended";
        }
		
		
		
        if(currentDate > startDate && currentDate < endDate)
        {
            // Start the timer
            timer = true;
        }
        else
        {
            // Stop the timer
            timer = false;
        }
    }
	
}
