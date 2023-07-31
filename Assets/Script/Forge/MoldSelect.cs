using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoldSelect : MonoBehaviour
{
    [SerializeField] MoldController moldController;
    [SerializeField] Mold mold;

    public void SelectMold()
    {
        moldController.SetMold(mold);
    }
}
