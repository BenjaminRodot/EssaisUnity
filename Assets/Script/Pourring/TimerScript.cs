using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] internal ForgeMiniGamePourring forgeMiniGamePourring;

    public TextMeshProUGUI countdownText;
    public float countdownTime = 10f;

    private float currentTime = 0f;
    private bool isCountingDown = false;

    private void Start()
    {
        currentTime = countdownTime;
        UpdateCountdownText();
    }

    private void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isCountingDown = false;
                ScenesManager.instance.LoadScene(ScenesManager.Scene.BladeShapingScene);
            }

            UpdateCountdownText();
        }
    }

    private void UpdateCountdownText()
    {
        countdownText.text = currentTime.ToString("0.00")+"s";
    }

    public void StartCountdown()
    {
        isCountingDown = true;
    }
}
