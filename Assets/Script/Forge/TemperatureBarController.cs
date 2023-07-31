using System;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class TemperatureBarController : MonoBehaviour
{
    [SerializeField] internal ForgeMiniGameMolten forgeMiniGameMolten;

    // Temperature variable
    private int level1 = 40;
    private int level2 = 60;
    private int level3 = 100;

    private Color color1 = Color.red;
    private Color color2 = Color.yellow;
    private Color color3 = Color.white;
    private Color color4 = new Color(0, 255, 255);

    [SerializeField] GameObject temperatureBar;
    [SerializeField] float maxTemperatureFurnace = 10000;
    [SerializeField] float minTemperatureFurnace = 0;
    [SerializeField] float moltenDifficulty = 3;

    private Slider temperatureSlider;
    private Transform temperatureArea;
    private float temperatureHeatingVelocity;
    private float temperatureHeatingPower = 0.015f;
    private float temperatureCoolingPower = 0.01f;
    private float temperatureValue;
    private float temperatureToMeltPercent;
    private float temperatureAreaPosX;
    private float minTemperatureAreaPosX = 25f;
    private float maxTemperatureAreaPosX = 425f;
    private float difficultytemperatureAreaRange = 25;

    private void Start()
    {
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureArea = temperatureBar.transform.Find("TemperatureArea");

        temperatureBar.SetActive(false);
    }
    internal void StartMolten()
    {
        temperatureSlider = temperatureBar.transform.Find("Slide").GetComponent<Slider>();
        temperatureArea.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(moltenDifficulty * difficultytemperatureAreaRange, 80);
        temperatureArea.gameObject.GetComponent<BoxCollider2D>().size = temperatureArea.gameObject.GetComponent<RectTransform>().sizeDelta;

        temperatureBar.SetActive(true);
        temperatureToMeltPercent = (forgeMiniGameMolten.GetMoltenTemperature(forgeMiniGameMolten.mineralsToMolten) - minTemperatureFurnace) / (maxTemperatureFurnace - minTemperatureFurnace);
        temperatureAreaPosX = (temperatureToMeltPercent * (maxTemperatureAreaPosX - minTemperatureAreaPosX));
        
        var pos = temperatureArea.transform.localPosition;
        pos.x = temperatureAreaPosX - 225; //size temperatureBar/2
        temperatureArea.transform.localPosition = pos;
    }

    internal void UpdateTemperatureBar(bool isTimeLeft)
    {
        if (isTimeLeft)
        {
            Maintaintemperature();
        }
        else
        {
            temperatureSlider.value = 0;
        }
        temperatureBar.SetActive(isTimeLeft);
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
        if (Input.GetKey(KeyCode.Space))
        {
            temperatureHeatingVelocity += temperatureHeatingPower * Time.deltaTime;
        }
        temperatureHeatingVelocity -= temperatureCoolingPower * Time.deltaTime;

        temperatureValue += temperatureHeatingVelocity;
        temperatureValue = Math.Clamp(temperatureValue, 0, 1);
        temperatureSlider.value = temperatureValue * 100;
        if (temperatureValue <= 0 || temperatureValue >= 1)
        {
            temperatureHeatingVelocity = 0f;
        }
    }

    internal bool IsCollided()
    {
        return temperatureArea.GetComponent<CollisionScript>().IsCollided;
    }
}
