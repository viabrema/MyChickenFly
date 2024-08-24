using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Camera mainCamera; // Referência à câmera principal
    public float cameraShakeDuration = 0.2f; // Duração do tremor da câmera
    public float cameraShakeMagnitude = 0.1f; // Intensidade do tremor da câmera
    public GameObject featherParticlesPrefab; // Prefab das partículas de penas
    private Vector3 originalOffset; // Armazena o offset original

    void Start()
    {
        // Atribuir a câmera principal se não estiver configurada no Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Guardar o offset original da câmera em relação ao jogador
        originalOffset = mainCamera.transform.position - GameObject.FindGameObjectWithTag("Chicken").transform.position;

        // Exemplo de como encontrar o prefab de partículas de penas na cena, se necessário
        featherParticlesPrefab = GameObject.Find("Feathers");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se o objeto que colidiu é a galinha
        if (collision.gameObject.CompareTag("Chicken"))
        {
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
    }
}
