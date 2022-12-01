using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    float timeLeft, totalTime;
    public float startingTime;
    bool canCountDown = false;
    float multiplier = 1;
    public float stoppedMultiplier = 0, multiplierInc;
    public float timeToAdd;
    [HideInInspector] public float powerupMultiplier = 1;

    public TextMeshProUGUI text;

    public GameEvent timeEnd;

    public GameOverCanvas gameoverCanvas;

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
                timeLeft -= Time.deltaTime * multiplier * powerupMultiplier;

                totalTime += Time.deltaTime * multiplier * powerupMultiplier;

                multiplier += multiplierInc;
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
        gameoverCanvas.gameObject.SetActive(true);
        gameoverCanvas.SetText(totalTime);
        timeEnd.Raise();
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

    public float GetTime
    {
        get { return timeLeft; }
        set { timeLeft = value; }
    }
}
