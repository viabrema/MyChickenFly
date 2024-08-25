using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public Text jumps;
    public Text time;
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
    // Start is called before the first frame update
    void Start()
    {
        jumps.text = "0";
        jumps.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        jumps.text = GameManager.instance.jumps.ToString();
        time.text = GameManager.instance.GetFormattedTime();
    }
}
