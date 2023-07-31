using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoltenLiquid
{
    public int durabilityModifier;
    public int damageModifier;
    public int priceModifier;
    public int enchantModifier;

    public override string ToString()
    {
        return "durabilityModifier = " + durabilityModifier + "\n" +
                "damageModifier = " + damageModifier + "\n" +
                "priceModifier = " + priceModifier + "\n" +
                "enchantModifier = " + enchantModifier + "\n";
    }

    public MoltenLiquid(int durabilityModifier, int damageModifier, int priceModifier, int enchantModifier)
    {
        this.durabilityModifier = durabilityModifier;
        this.damageModifier = damageModifier;
        this.priceModifier = priceModifier;
        this.enchantModifier = enchantModifier;
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
    }
}