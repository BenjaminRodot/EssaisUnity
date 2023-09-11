using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of GameManager found !");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] BeatScroller m_BeatScroller;
    [SerializeField] TextMeshProUGUI comboTextMeshProUI;
    [SerializeField] TextMeshProUGUI scoreTextMeshProUI;
    [SerializeField] private Image test;

    private bool startPlaying;
    private int combo = 1;
    private int score = 0;
    private Color32[] listColor = { new Color32(140, 169, 237, 255), new Color32(233, 237, 176, 255), new Color32(241, 209, 98, 255), new Color32(215, 129, 255, 255), new Color32(188, 73, 104, 255) };
    

    private void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                m_BeatScroller.StartScroll();

                //m_AudioSource.Play();
            }
        }
    }

    public void NoteHit()
    {
        score += combo;
        if(combo>1) 
        {
            comboTextMeshProUI.gameObject.SetActive(true);
        }
        if (combo < 4)
        {
            combo++;
            comboTextMeshProUI.text = "x" + combo;
        };
        scoreTextMeshProUI.text = score.ToString();
        comboTextMeshProUI.color = listColor[combo];
    }

    public void NoteMiss()
    {
        comboTextMeshProUI.gameObject.SetActive(false);
        combo = 1;
        Debug.Log("Miss");
    }
}
