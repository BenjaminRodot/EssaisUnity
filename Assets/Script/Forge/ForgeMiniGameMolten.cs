using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //DontDestroyOnLoad(this);
    }
    #endregion

    internal List<Mineral> mineralsToMolten;
    [SerializeField] internal TimerBarController timerBarController;
    [SerializeField] internal TemperatureBarController temperatureBarController;
    [SerializeField] internal MoldController moldController;

    [SerializeField] private Button burnButton;
    [SerializeField] private Image imageColorMinerals;
    private bool gameStarted = false;

    // Score
    private int currentScore = 0;
    private int scoreMax = 1;
    private bool beginningScoring = false;

    public void StartBurn()
    {
        beginningScoring = false;
        mineralsToMolten = SelectedItem.instance.mineralsDroped;
        gameStarted = true;

        timerBarController.StartTimer();
        temperatureBarController.StartMolten();
    }

    private void Update()
    {
        moldController.UpdateMold();
        timerBarController.UpdateTimerBar();
        temperatureBarController.UpdateTemperatureBar(gameStarted);
        if (gameStarted)
        {
            if (timerBarController.IsTimeLeft())
            {
                isTemperatureInArea();
                if (beginningScoring)
                {
                    scoreMax++;
                    if (temperatureBarController.IsCollided())
                    {
                        currentScore++;
                    }
                }
            }
            else
            {
                Debug.Log("scoreMax : " + scoreMax);
                Debug.Log("currentScore : " + currentScore);
                Debug.Log(currentScore * 100 / scoreMax + "%");
                beginningScoring = false;
                scoreMax = 1;
                currentScore = 0;
                gameStarted = false;
                MolteMinerals();
            }
        }
        
    }

    internal int GetMoltenTemperature(List<Mineral> minerals)
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
            beginningScoring = temperatureBarController.IsCollided();
        }
        return beginningScoring;
    }

    private void MolteMinerals()
    {
        MoltenLiquid moltenLiquid = new MoltenLiquid(mineralsToMolten);
        imageColorMinerals.color = moltenLiquid.GetColor();
        CurrentMoltenMinerals.SetMoltenLiquid(moltenLiquid);
        Debug.Log(imageColorMinerals.color);
        SelectedItem.instance.LoseDroppedItem();
        ScenesManager.instance.LoadScene(ScenesManager.Scene.PourringScene);
        //Debug.Log(moltenLiquid.ToString());
    }
}
