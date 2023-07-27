using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minerals", menuName = "Inventory/Minerals")]
[System.Serializable]
public class Minerals:Item
{
    public int moltentemperature;
}