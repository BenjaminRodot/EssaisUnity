using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
[System.Serializable]
public class Item:ScriptableObject
{
    public int value;
    public Sprite image;
    public int maxStack;
}