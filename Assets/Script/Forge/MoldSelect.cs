using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoldSelect : MonoBehaviour
{
    [SerializeField] Mold mold;
    [SerializeField] TextMeshProUGUI moldSelectedText;
    [SerializeField] TextMeshProUGUI mineralNeededText;

    public void SelectMold()
    {
        ForgeMiniGameMolten.instance.SetMold(mold);
        moldSelectedText.text = "Mold : " + mold.name;
    }
}
