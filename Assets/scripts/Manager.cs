using UnityEngine;

public class GameManager : MonoBehaviour
{
    // A instância estática do GameManager
    public static GameManager instance;

    // Exemplo de dados que o GameManager pode gerenciar
    public int playerScore = 0;
    public int jumps = 0;

    // Variáveis para o temporizador
    private float elapsedTime = 0f;  // Tempo total em segundos
    private bool isTimerRunning = false;  // Controle do estado do temporizador

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

    void Update()
    {
        // Se o temporizador estiver rodando, incrementa o tempo
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
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

    // Função para iniciar ou continuar o temporizador
    public void PlayTimer()
    {
        isTimerRunning = true;
        Debug.Log("Timer started");
    }

    // Função para pausar o temporizador
    public void PauseTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer paused");
    }

    // Função para parar o temporizador e resetar o tempo
    public void StopTimer()
    {
        isTimerRunning = false;
        elapsedTime = 0f;
        Debug.Log("Timer stopped and reset");
    }

    // Função para resetar o temporizador, mas sem parar
    public void ResetTimer()
    {
        elapsedTime = 0f;
        Debug.Log("Timer reset");
    }

    // Função para obter o tempo atual formatado como minutos e segundos
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Função para obter o tempo total em segundos
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
