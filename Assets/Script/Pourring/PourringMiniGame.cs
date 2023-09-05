using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PourringMiniGame : MonoBehaviour
{
    [SerializeField] private Collider2D colliderIn;
    [SerializeField] private Collider2D colliderOut;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public void Update()
    {
        textMeshProUGUI.text = colliderIn.GetComponent<CountCollision>().GetCount().ToString();
    }

}
