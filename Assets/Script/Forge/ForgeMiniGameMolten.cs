using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ForgeMiniGameMolten : MonoBehaviour
{
    #region Singleton
    public static ForgeMiniGameMolten instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of ForgeMiniGameMolten found !");
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    private float minTemperatureAreaPosX = 25f;
    private float maxTemperatureAreaPosX = 425f;

    // Timer
    public GameObject timerBar;
    public float timeInteract = 2f;

    private List<Mineral> mineralsToMolten;
    private bool isMoltRunning = false;
    private float timeLeft;
    private Slider timerSlider;
    
    // Temperature variable
    private int level1 = 40;
    private int level2 = 60;
    private int level3 = 100;

    private Color color1 = Color.red;
    private Color color2 = Color.yellow;
    private Color color3 = Color.white;
    private Color color4 = new Color(0, 255, 255);

    // Temperaturee object
    public GameObject temperatureBar;
    public float maxTemperatureFurnace = 10000;
    public float minTemperatureFurnace = 0;
    public float temperatureAreaRange = 2;

    private Slider temperatureSlider;
    private Transform temperatureArea;
    private float temperatureHeatingVelocity;
    private float temperatureHeatingPower = 0.015f;
    private float temperatureCoolingPower = 0.01f;
    private float temperatureValue;
    private float temperatureToMeltPercent;
    private float temperatureAreaPosX;

    // Score
    private int currentScore = 0;
    private int scoreMax = 0;
    private int scoreFinal = 0;

    public void Start()
    {
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureArea = temperatureBar.transform.Find("TemperatureArea");
    }
    public void OnButtonClick()
    {
        isMoltRunning = true;
        timeLeft = timeInteract;
        timerSlider = timerBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        mineralsToMolten = SelectedItem.instance.mineralsDroped;
        temperatureArea.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(temperatureAreaRange, 100);

        temperatureToMeltPercent = (GetMoltenTemperatureAverage(mineralsToMolten) - minTemperatureFurnace) / (maxTemperatureFurnace - minTemperatureFurnace);
        temperatureAreaPosX = (temperatureToMeltPercent * (maxTemperatureAreaPosX - minTemperatureAreaPosX));
        
        var pos = temperatureArea.transform.localPosition;
        pos.x = temperatureAreaPosX -225; //size temperatureBar /2
        temperatureArea.transform.localPosition = pos;

        float areaInPercent = temperatureAreaRange / (maxTemperatureAreaPosX - minTemperatureAreaPosX);

        //Debug.Log("temperatureToMeltPercent : " + temperatureToMeltPercent +"%");
        //Debug.Log("areaPercent : " + (temperatureAreaRange / (maxTemperatureAreaPosX - minTemperatureAreaPosX)) + "%");
        //Debug.Log("areaPercentMin : " + (temperatureToMeltPercent - areaInPercent / 2) + "%");
        //Debug.Log("areaPercentMax : " + (temperatureToMeltPercent + areaInPercent / 2) + "%");
    }

    private void Update()
    {
        Maintaintemperature();
        
        if (isMoltRunning)
        {
            timerBar.SetActive(true);
            temperatureBar.SetActive(true);

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerSlider.value = (1 - timeLeft / timeInteract) * 100;
                if (isTemperatureInArea())
                {
                    currentScore++;
                    Debug.Log("Dedans");
                }
                else
                {
                    Debug.Log("Pas dedans");
                }
                scoreMax++;
            }
            else
            {
                timerSlider.value = 0;
                temperatureSlider.value = 0;
                scoreFinal = currentScore * 100 * 10 / 9 / scoreMax;

                //Debug.Log("scoreMax : " + scoreMax);
                //Debug.Log("currentScore : " + currentScore);
                //Debug.Log(currentScore * 100 / scoreMax + "%");
                scoreMax = 0;
                currentScore = 0;
                timerBar.SetActive(false);
                temperatureBar.SetActive(false);
                isMoltRunning = false;
                MolteMinerals();
                //Debug.Log(scoreFinal + "%");
            }
        }
    }

    private int GetMoltenTemperatureAverage(List<Mineral> minerals)
    {
        int averageTemperature = 0;
        foreach(Mineral mineral in minerals)
        {
            averageTemperature += mineral.moltentemperaturee;
        }

        return averageTemperature/ minerals.Count;
    }

    private bool isTemperatureInArea()
    {
        float areaInPercent = temperatureAreaRange/(maxTemperatureAreaPosX-minTemperatureAreaPosX);
        return (temperatureSlider.value >= ((temperatureToMeltPercent - areaInPercent / 2) *100)) && (temperatureSlider.value <= ((temperatureToMeltPercent + areaInPercent / 2) * 100));
    }

    private void MolteMinerals()
    {
        MoltenLiquid moltenLiquid = new MoltenLiquid(mineralsToMolten);
        SelectedItem.instance.CleanDroppedItem();
        //Debug.Log(moltenLiquid.ToString());
    }

    private void Maintaintemperature()
    {
        
        if (temperatureSlider.value < 40)
        {
            temperatureBar.transform.Find("Slide").GetChild(0).GetComponent<Image>().color = Color.Lerp(color1, color2, temperatureSlider.value / level1);
        }
        else if (temperatureSlider.value < 60)
        {
            temperatureBar.transform.Find("Slide").GetChild(0).GetComponent<Image>().color = Color.Lerp(color2, color3, (temperatureSlider.value - level1) / (level2 - level1));
        }
        else if (temperatureSlider.value <= 100)
        {
            temperatureBar.transform.Find("Slide").GetChild(0).GetComponent<Image>().color = Color.Lerp(color3, color4, (temperatureSlider.value - level2) / (level3 - level2));
        }

        if(Input.GetKey(KeyCode.Space))
        {
            temperatureHeatingVelocity += temperatureHeatingPower * Time.deltaTime;
        }
        temperatureHeatingVelocity -= temperatureCoolingPower * Time.deltaTime;

        temperatureValue += temperatureHeatingVelocity;
        temperatureValue = Math.Clamp(temperatureValue, 0, 1);
        temperatureSlider.value = temperatureValue*100;
        if(temperatureValue<=0 || temperatureValue >= 1)
        {
            temperatureHeatingVelocity = 0f;
        }
        //Debug.Log(temperatureHeatingVelocity);
    }

}
