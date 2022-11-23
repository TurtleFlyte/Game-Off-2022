using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    float timeLeft, totalTime;
    public float startingTime;
    bool canCountDown = true;
    float multiplier = 1, stoppedMultiplier = 0;
    public float timeToAdd;

    public TextMeshProUGUI text;

    void Start()
    {
        timeLeft = startingTime;
    }

    void Update()
    {
        if(timeLeft > 0)
        {
            if (canCountDown)
            {
                timeLeft -= Time.deltaTime * multiplier;

                totalTime += Time.deltaTime * multiplier;

                multiplier += 0.0001f;
            }
            else
            {
                timeLeft -= Time.deltaTime * stoppedMultiplier;

                totalTime += Time.deltaTime * stoppedMultiplier;
            }
        }
        else if(canCountDown)
        {
            TimeEnded();
        }

        DisplayTime();
    }

    public void AddTime()
    {
        Debug.Log("add time");
        timeLeft += timeToAdd;
        canCountDown = true;
    }

    void TimeEnded()
    {
        Debug.Log("time ended");
        timeLeft = 0;
        canCountDown = false;
    }

    // Setter for canCountDown
    public bool CanCountDown
    {
        set { canCountDown = value; }
    }

    // Formats the time then puts it into the text
    void DisplayTime()
    {
        if(timeLeft <= 0)
        {
            text.text = string.Format("{0:00}:{1:00}", 0, 0);

            return;
        }
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
