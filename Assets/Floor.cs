using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Camera mainCamera; // Referência à câmera principal
    public float cameraShakeDuration = 0.2f; // Duração do tremor da câmera
    public float cameraShakeMagnitude = 0.1f; // Intensidade do tremor da câmera
    public GameObject featherParticlesPrefab; // Prefab das partículas de penas

    void Start()
    {
        // Atribuir a câmera principal se não estiver configurada no Inspector
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Exemplo de como encontrar o prefab de partículas de penas na cena, se necessário
        featherParticlesPrefab = GameObject.Find("Feathers");
        // Nota: Apenas descomente a linha acima e substitua "FeatherParticlesPrefabNameHere" pelo nome correto do prefab,
        // se quiser encontrar o prefab na cena. Certifique-se de que o prefab está na cena ou configurado via script.
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
        Vector3 originalPosition = mainCamera.transform.position;
        float elapsed = 0.0f;

        while (elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * cameraShakeMagnitude;
            float y = Random.Range(-1f, 1f) * cameraShakeMagnitude;

            mainCamera.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Retornar a câmera para a posição original
        mainCamera.transform.position = originalPosition;
    }
}
