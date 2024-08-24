using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar a cena

public class Chick : MonoBehaviour
{
    public string sceneName; // Nome da cena que você deseja carregar

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
