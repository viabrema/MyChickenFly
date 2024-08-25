using UnityEngine;

public class GameManager : MonoBehaviour
{
    // A instância estática do GameManager
    public static GameManager instance;

    // Exemplo de dados que o GameManager pode gerenciar
    public int playerScore = 0;
    public int jumps = 0;

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

    public void AddScore(int value)
    {
        playerScore += value;
        Debug.Log("New score: " + playerScore);
    }

    public void AddJump()
    {
        jumps++;
        Debug.Log("Jumps: " + jumps);
    }

}
