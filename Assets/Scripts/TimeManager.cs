using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float remainingTime = 1f;
    public TextMeshProUGUI timeText;
    public bool isTimeRunning = false;
    public UnityEvent onTimeEnd;
    public float defaultTime = 15f;
    
    private void Awake() {
        instance = this;
    }

    public void StartTimer()
    {
        isTimeRunning = true;
    }

    public void PauseTimer()
    {
        isTimeRunning = false;
    }

    public void ResetTimer()
    {
        remainingTime = defaultTime;
        isTimeRunning = false;
    }

    public void LevelCleared()
    {
        remainingTime += defaultTime;
    }

    public void AddTime(float time)
    {
        remainingTime += time;
    }

    public void RemoveTime(float time)
    {
        remainingTime -= time;
    }

    private void Update() {
        if(isTimeRunning)
        {
            if(remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                timeText.text = remainingTime.ToString("F2");
            }
            else
            {
                remainingTime = 0;
                isTimeRunning = false;
                onTimeEnd?.Invoke();
            }
        }
    }
}
