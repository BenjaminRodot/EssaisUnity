using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TimerBarController : MonoBehaviour
{
    [SerializeField] internal ForgeMiniGameMolten forgeMiniGameMolten;
    [SerializeField] GameObject timerBar;
    [SerializeField] Slider timerSlider;

    [SerializeField] float timeInteract = 10f;

    private float timeLeft;

    private void Start()
    {
        timerBar.SetActive(false);
    }

    internal void StartTimer()
    {
        timeLeft = timeInteract;
        timerBar.SetActive(true);
    }

    internal void UpdateTimerBar()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = (1 - timeLeft / timeInteract) * 100;
        }
        else
        {
            timerSlider.value = 0;
            timerBar.SetActive(false);
        }
    }

    internal bool IsTimeLeft()
    { 
        return timeLeft >= 0; 
    }
}
