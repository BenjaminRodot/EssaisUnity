using UnityEngine;

[CreateAssetMenu(fileName = "New Mold", menuName = "Knowledge/Mold")]
[System.Serializable]
public class Mold:ScriptableObject
{
    public int value;
    public Sprite image;
    public int nbMineralsNeeded;
    public int poureringDifficulty;
    public int hammeringDiffiulty;
}
