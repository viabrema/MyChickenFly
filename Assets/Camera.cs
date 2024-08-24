using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavização
    public Vector3 offset; // Deslocamento da câmera em relação ao jogador
    public bool cameraFollow = false;
    public float minX = -10f; // Limite mínimo X
    public float maxX = 10f;  // Limite máximo X
    public float minY = -5f;  // Limite mínimo Y
    public float maxY = 5f;   // Limite máximo Y

    void Update()
    {
        if (cameraFollow)
        {
            // Calcular a posição desejada com base na posição do jogador
            var desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Limitar a posição X para que não ultrapasse os limites horizontais
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);

            // Limitar a posição Y para que não ultrapasse os limites verticais
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Atualizar a posição desejada com os valores X e Y limitados
            desiredPosition = new Vector3(clampedX, clampedY, transform.position.z);

            // Suavizar o movimento da câmera em direção à posição desejada
            var smoothedPosition = Vector3.MoveTowards(transform.position, desiredPosition, 10f);
            transform.position = smoothedPosition;
        }
    }
}
