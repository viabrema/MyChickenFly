using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour
{

    public static StatsScreen instance;
    public Text jumpsText;
    public Text timeText;
    public string nextScene;
    public string currentScene;
    public GameObject statsScreen;
    void Awake()
    {
        // Verifica se já existe uma instância do GameManager
        if (instance == null)
        {
            // Se não existir, esta instância será a única e não será destruída ao carregar uma nova cena
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existir uma instância, destrua este GameObject para evitar duplicações
            Destroy(gameObject);
        }
    }

    public void SetJumps(int jumps)
    {
        jumpsText.text = jumps.ToString();
    }

    public void SetTime(string time)
    {
        timeText.text = time;
    }
    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }

    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    public void show()
    {
        // Ativa a HUD
        statsScreen.SetActive(true);
        jumpsText.text = GameManager.instance.jumps.ToString();
        timeText.text = GameManager.instance.GetFormattedTime();
    }

    public void Hide()
    {
        // Desativa a HUD
        statsScreen.SetActive(false);
    }

    public void NextLevelClick()
    {
        Debug.Log("NextLevelClick");
        LoadNextScene();
        Hide();
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
