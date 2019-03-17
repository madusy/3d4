using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private DateTime timeBegin;
    private TimeSpan timeTimer;

    void Start()
    {
        timeBegin = DateTime.Now;
    }

    private void Update()
    {
        timeTimer = DateTime.Now - timeBegin;
        Debug.Log(timeTimer.Seconds);
    }

    public int GetMinutes()
    {
        return timeTimer.Minutes;
    }

    public int GetSeconds()
    {
        return timeTimer.Seconds;
    }
}
