using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar a cena

public class Chick : MonoBehaviour
{
    GameObject winSound; // Referência ao AudioSource do som de vitória
    public string sceneName; // Nome da cena que você deseja carregar

    GameObject soundtrack_01; // Referência ao AudioSource da trilha sonora 01

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            // Parar a trilha sonora
            soundtrack_01.GetComponent<AudioSource>().Stop();
            // Tocar o som de vitória
            winSound.GetComponent<AudioSource>().Play();
            // Iniciar a corrotina para trocar de cena após 3 segundos
            StartCoroutine(SwitchSceneAfterDelay(1f));
        }
    }

    IEnumerator SwitchSceneAfterDelay(float delay)
    {
        // Espera pelo tempo especificado
        yield return new WaitForSeconds(delay);

        // Carrega a cena especificada
        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Encontra o AudioSource da trilha sonora
        soundtrack_01 = GameObject.Find("Soundtrack_01");
        // Encontra o AudioSource do som de vitória na cena
        winSound = GameObject.Find("WinSound");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
