using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeMiniGamePourring : MonoBehaviour
{
    [SerializeField] Material liquidMaterial;

    [SerializeField] private Collider2D colliderIn;
    [SerializeField] private Collider2D colliderOut;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private float countIn;
    private float countOut;

    // Update is called once per frame
    void Update()
    {
        if (CurrentMoltenMinerals.GetMoltenLiquid() != null)
        {
            this.gameObject.GetComponent<Image>().color = CurrentMoltenMinerals.GetMoltenLiquid().GetColor();
            liquidMaterial.color = CurrentMoltenMinerals.GetMoltenLiquid().GetColor();
        }

        countIn = (float)colliderIn.GetComponent<CountCollision>().GetCount();
        countOut = (float)colliderOut.GetComponent<CountCollision>().GetCount();

        textMeshProUGUI.text = ((int)(countIn/(countIn+countOut*10)*100)).ToString();
    }
}
