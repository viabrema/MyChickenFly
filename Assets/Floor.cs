using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Camera mainCamera; // Referência à câmera principal
    float cameraShakeDuration = 0.1f; // Duração do tremor da câmera
    float cameraShakeMagnitude = 0.05f; // Intensidade do tremor da câmera
    public GameObject featherParticlesPrefab; // Prefab das partículas de penas
    private Vector3 originalOffset; // Armazena o offset original
    private bool isShaking = false; // Controla se a câmera está tremendo
    float shakeCooldown = 2.0f; // Tempo de espera entre os tremores
    GameObject bumpSound; // Referência ao AudioSource do som de colisão

    void Start()
    {
        // Atribuir a câmera principal se não estiver configurada no Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        bumpSound = GameObject.Find("BumSound");
        // Exemplo de como encontrar o prefab de partículas de penas na cena, se necessário
        featherParticlesPrefab = GameObject.Find("Feathers");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        originalOffset = mainCamera.transform.position - GameObject.FindGameObjectWithTag("Chicken").transform.position;

        // Verificar se o objeto que colidiu é a galinha
        if (collision.gameObject.CompareTag("Chicken") && !isShaking)
        {
            bumpSound.GetComponent<AudioSource>().Play();
            // Iniciar o tremor da câmera
            StartCoroutine(ShakeCamera());

            // Instanciar as partículas de penas na posição da colisão
            if (featherParticlesPrefab != null)
            {
                var particles = Instantiate(featherParticlesPrefab, collision.contacts[0].point, Quaternion.identity);
                Destroy(particles, 1.0f);
            }
        }
    }

    IEnumerator ShakeCamera()
    {
        isShaking = true; // Indica que o tremor está em andamento

        Vector3 elapsedOffset = originalOffset;
        float elapsed = 0.0f;

        while (elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * cameraShakeMagnitude;
            float y = Random.Range(-1f, 1f) * cameraShakeMagnitude;

            elapsedOffset = new Vector3(originalOffset.x + x, originalOffset.y + y, originalOffset.z);

            // Atualizar a posição da câmera com o novo offset
            mainCamera.transform.position = GameObject.FindGameObjectWithTag("Chicken").transform.position + elapsedOffset;

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Retornar o offset da câmera para o valor original
        mainCamera.transform.position = GameObject.FindGameObjectWithTag("Chicken").transform.position + originalOffset;

        yield return new WaitForSeconds(shakeCooldown); // Espera o tempo de cooldown
        isShaking = false; // Indica que o tremor terminou e pode começar outro
    }
}
