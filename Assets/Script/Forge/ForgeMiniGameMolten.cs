using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject timerBar;
    private float timeInteract = 2f;

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
    [SerializeField] private Button burnButton;
    [SerializeField] private GameObject temperatureGame;
    [SerializeField] private GameObject temperatureBar;
    [SerializeField] private float maxTemperatureFurnace = 10000;
    [SerializeField] private float minTemperatureFurnace = 0;
    [SerializeField] private float temperatureAreaRange = 2;

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
    private int scoreMax = 1;
    private bool beginningScoring = false;

    // Mold
    private Mold mold;
    private TextMeshProUGUI mineralNeededText;

    public void Start()
    {
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureArea = temperatureBar.transform.Find("TemperatureArea");

        temperatureGame.SetActive(false);
        burnButton.interactable = false;
    }
    public void StartBurn()
    {
        //===============\\
        //  Find Object  \\
        //===============\\
        timerSlider = timerBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureArea.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(temperatureAreaRange, 80);
        temperatureArea.gameObject.GetComponent<BoxCollider2D>().size = temperatureArea.gameObject.GetComponent<RectTransform>().sizeDelta;

        //========================\\
        //  Instanciate variable  \\
        //========================\\
        beginningScoring = false;
        timeLeft = timeInteract;

        temperatureGame.SetActive(true);
        isMoltRunning = true;
        mineralsToMolten = SelectedItem.instance.mineralsDroped;
        temperatureToMeltPercent = (GetMoltenTemperature(mineralsToMolten) - minTemperatureFurnace) / (maxTemperatureFurnace - minTemperatureFurnace);
        temperatureAreaPosX = (temperatureToMeltPercent * (maxTemperatureAreaPosX - minTemperatureAreaPosX));
        
        var pos = temperatureArea.transform.localPosition;
        pos.x = temperatureAreaPosX - 225; //size temperatureBar/2
        temperatureArea.transform.localPosition = pos;
    }

    private void Update()
    {
        if(mold!=null)
        {
            mineralNeededText.text = "Mineral needed : " + (mold.nbMineralsNeeded - SelectedItem.instance.mineralsDroped.Count);
            if(mold.nbMineralsNeeded <= SelectedItem.instance.mineralsDroped.Count)
            {
                burnButton.interactable = true; 
            }
           
        }
        else
        {
            burnButton.interactable = false;
        }
        if (isMoltRunning)
        {
            timerBar.SetActive(true);
            temperatureBar.SetActive(true);

            if (timeLeft > 0)
            {
                Maintaintemperature();
                isTemperatureInArea();
                timeLeft -= Time.deltaTime;
                timerSlider.value = (1 - timeLeft / timeInteract) * 100;
                if(beginningScoring)
                {
                    scoreMax++;
                    if (temperatureArea.GetComponent<CollisionScript>().IsCollided)
                    {
                        currentScore++;
                    }
                }
                
            }
            else
            {
                timerSlider.value = 0;
                temperatureSlider.value = 0;

                Debug.Log("scoreMax : " + scoreMax);
                Debug.Log("currentScore : " + currentScore);
                Debug.Log(currentScore * 100 / scoreMax + "%");
                scoreMax = 1;
                currentScore = 0;
                timerBar.SetActive(false);
                temperatureBar.SetActive(false);
                isMoltRunning = false;
                MolteMinerals();
            }
        }
    }

    private int GetMoltenTemperature(List<Mineral> minerals)
    {
        int maxTemperature = 0;
        foreach(Mineral mineral in minerals)
        {
            if(maxTemperature < mineral.moltentemperaturee)
            {
                maxTemperature = mineral.moltentemperaturee;
            }
        }
        return maxTemperature;
    }

    private bool isTemperatureInArea()
    {
        if(!beginningScoring)
        {
            beginningScoring = temperatureArea.GetComponent<CollisionScript>().IsCollided;
        }
        return beginningScoring;
    }

    private void MolteMinerals()
    {
        MoltenLiquid moltenLiquid = new MoltenLiquid(mineralsToMolten);
        SelectedItem.instance.LoseDroppedItem();
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
    }

    public void SetMold(Mold mold)
    {
        this.mold = mold;
    }
}
