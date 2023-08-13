using UnityEngine;

public class CurrentMoltenMinerals : MonoBehaviour
{
    public static MoltenLiquid currentMoltenLiquid;

    public static void SetMoltenLiquid(MoltenLiquid moltenLiquid)
    {
        currentMoltenLiquid = moltenLiquid;
    }

    public static MoltenLiquid GetMoltenLiquid()
    {
        return currentMoltenLiquid;
    }
}
