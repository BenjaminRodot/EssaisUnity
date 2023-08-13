using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoltenLiquid
{
    private int durabilityModifier;
    private int damageModifier;
    private int priceModifier;
    private int enchantModifier;

    private Color color;

    public override string ToString()
    {
        return "durabilityModifier = " + durabilityModifier + "\n" +
                "damageModifier = " + damageModifier + "\n" +
                "priceModifier = " + priceModifier + "\n" +
                "enchantModifier = " + enchantModifier + "\n";
    }

    public MoltenLiquid(int durabilityModifier, int damageModifier, int priceModifier, int enchantModifier, Color color)
    {
        this.durabilityModifier = durabilityModifier;
        this.damageModifier = damageModifier;
        this.priceModifier = priceModifier;
        this.enchantModifier = enchantModifier;
        this.color = color;
    }

    public MoltenLiquid(List<Mineral> minerals)
    {
        int durabilityModifier = 0;
        int damageModifier = 0;
        int priceModifier = 0;
        int enchantModifier = 0;

        foreach(Mineral mineral in minerals)
        {
            durabilityModifier += mineral.durabilityModifier;
            damageModifier += mineral.damageModifier;
            priceModifier += mineral.priceModifier;
            enchantModifier += mineral.enchantModifier;
        }

        this.durabilityModifier = durabilityModifier;
        this.damageModifier = damageModifier;
        this.priceModifier = priceModifier;
        this.enchantModifier = enchantModifier;
        this.color = AverageColorFromTexture(minerals);
    }

    private Color AverageColorFromTexture(List<Mineral> minerals)
    {
        int total = minerals.Count;
        float r = 0;
        float g = 0;
        float b = 0;
        foreach (Mineral mineral in minerals)
        {
            Color color = mineral.AverageColorFromTexture();
            r += color.r;
            g += color.g;
            b += color.b;
        }
        return new Color(r / total,g / total,b / total, 255);
    }

    public Color GetColor()
    {
        return color;
    }
}