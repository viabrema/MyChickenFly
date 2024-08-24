using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavização
    public Vector3 offset; // Deslocamento da câmera em relação ao jogador

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position;
            // transform.rotation = Quaternion.identity;
        }
    }
}
