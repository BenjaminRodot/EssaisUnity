using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] private float beatTempo;
    [SerializeField] private bool hasStarted;
    private int cellSize = 100;

    private void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    private void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime * cellSize, 0f,0f);
        }
    }

    internal void StartScroll()
    {
        hasStarted = true;
    }
}
