using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    #region Singleton
    public static ScenesManager instance;
    private GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of ScenesManager found !");
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    public enum Scene
    {
        Scene1,
        ForgeScene1,
        PourringScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadDefaultScene()
    {
        SceneManager.LoadScene(Scene.Scene1.ToString());
    }

    private void Update()
    {

        if (player == null) 
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals(Scene.Scene1.ToString()))
            {
                player.SetActive(true);
            }
            else
            {
                player.SetActive(false);
            }
        }
        
    }
    /*
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/
}
