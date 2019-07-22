using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Timers : MonoBehaviour
{
    public Timer[] timers;

    public void StartTimer(int id)
    {
        Timer T = timers[id];
        T.started = true;
        T.currentTime = T.timerLength;
    }

    void Update()
    {
        float t = Time.deltaTime;
        foreach (Timer T in timers) {
            if (T.started) {
                T.currentTime -= t;
                if (T.currentTime > 0) {
                    if (T.Output != null) T.Output.text = T.currentTime.ToString();
                }
                else
                {
                    if (T.Output != null) T.Output.text = (0).ToString();
                    foreach (UnityEvent e in T.timerEndActions)
                    {
                        e.Invoke();
                    }
                }
            }
        }
    }
}

[Serializable]
public class Timer {
    #region Variables
    public float currentTime;
    public float timerLength;
    public bool started = false;
    public List<UnityEvent> timerEndActions;
    public Text Output;
    #endregion

    public Timer(float length) {
        timerLength = length;
        currentTime = length;
    }

    public Timer(float length, UnityEvent endTimerEvent)
    {
        timerLength = length;
        currentTime = length;
        timerEndActions.Add(endTimerEvent);
    }

    public Timer(float length, UnityEvent endTimerEvent, Text output)
    {
        timerLength = length;
        currentTime = length;
        timerEndActions.Add(endTimerEvent);
        Output = output;
    }
}
