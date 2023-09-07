using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeMiniGamePourring : MonoBehaviour
{
    #region Singleton
    public static ForgeMiniGamePourring instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of ForgeMiniGamePourring found !");
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    [SerializeField] internal TimerScript timerScript;
    [SerializeField] internal DragControl dragControl;

    [SerializeField] Material liquidMaterial;

    [SerializeField] private GameObject moltenMineralsColor;
    [SerializeField] private Collider2D colliderIn;
    [SerializeField] private Collider2D colliderOut;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject particlesParent;

    private float countIn;
    private float countOut;
    private int nbParticle;
    private float score = 0f;

    private void Start()
    {
        nbParticle = particlesParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentMoltenMinerals.GetMoltenLiquid() != null)
        {
            moltenMineralsColor.GetComponent<Image>().color = CurrentMoltenMinerals.GetMoltenLiquid().GetColor();
            liquidMaterial.color = CurrentMoltenMinerals.GetMoltenLiquid().GetColor();
        }

        countOut = (float)colliderOut.GetComponent<CountCollision>().GetCount();
        countIn = (float)colliderIn.GetComponent<CountCollision>().GetCount();

        score = (countIn - countOut* 4) / nbParticle * 100;
        score = System.Math.Clamp(score, 0, 100);
        textMeshProUGUI.text = ((int)score).ToString();
    }
}
