using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoldController : MonoBehaviour
{
    [SerializeField] internal ForgeMiniGameMolten forgeMiniGameMolten;

    [SerializeField] Button burnButton;
    [SerializeField] TextMeshProUGUI moldSelectedText;
    [SerializeField] TextMeshProUGUI mineralNeededText;
    private Mold mold;

    internal void UpdateMold()
    {
        if (mold != null)
        {
            mineralNeededText.text = "Mineral needed : " + (mold.nbMineralsNeeded - SelectedItem.instance.mineralsDroped.Count);
            if (mold.nbMineralsNeeded <= SelectedItem.instance.mineralsDroped.Count)
            {
                burnButton.interactable = true;
            }
            else
            {
                burnButton.interactable = false;
            }
        }
        else
        {
            burnButton.interactable = false;
        }
    }

    internal void SetMold(Mold mold)
    {
        this.mold = mold;
        moldSelectedText.text = "Mold : " + mold.name;
    }
}
