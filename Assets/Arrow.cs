using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform circle; // Referência ao círculo
    public float distance = 1.0f; // Distância do círculo
    public float rotationSpeed = 150f; // Velocidade de rotação
    public Transform arrow;
    private SpriteRenderer arrowSprite; // Referência ao SpriteRenderer da seta
    private bool isRotating = true; // Controla a rotação
    private bool isHoldingSpace = false; // Controla se a tecla espaço está sendo segurada
    private float holdTime = 0f; // Tempo que a tecla espaço foi segurada
    public float maxHoldTime = 3f; // Tempo máximo para segurar espaço (configurável)
    public float minForce = 10f; // Força mínima inicial (configurável)
    public float maxForce = 40f; // Força máxima (configurável)
    private Vector3 defaultScale = new Vector3(0.35f, 0.35f, 0.35f); // Escala padrão da seta
    public float maxScale = 2f; // Escala máxima da seta
    public float shakeIntensity = 0.1f; // Intensidade do tremor quando a força está próxima do máximo

    private Rigidbody2D circleRb; // Referência ao Rigidbody2D do círculo
    private Vector3 originalPosition; // Posição original para o tremor

    void Start()
    {
        // Obter a referência ao Rigidbody2D do círculo
        circleRb = circle.GetComponent<Rigidbody2D>();

        // Obter a referência ao SpriteRenderer da seta
        arrowSprite = arrow.GetComponent<SpriteRenderer>();

        // Configurar a escala padrão da seta
        arrow.localScale = defaultScale;

        // Salvar a posição original da seta
        originalPosition = arrow.localPosition;
    }

    void Update()
    {
        transform.position = circle.position;

        if (isRotating)
        {
            // Manter a seta girando em torno do círculo
            transform.RotateAround(circle.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // Verificar se a tecla espaço foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Parar a rotação enquanto a tecla espaço está sendo segurada
            isRotating = false;
            isHoldingSpace = true;
            holdTime = 0f; // Reiniciar o tempo de espera
        }

        // Verificar se a tecla espaço está sendo segurada
        if (isHoldingSpace)
        {
            holdTime += Time.deltaTime;

            // Limitar o tempo de espera ao tempo máximo configurado
            holdTime = Mathf.Min(holdTime, maxHoldTime);

            // Interpolar a força com base no tempo segurado
            float force = Mathf.Lerp(minForce, maxForce, holdTime / maxHoldTime);

            // Ajustar o tamanho da seta com base na força
            float scaleMultiplier = Mathf.Lerp(1f, maxScale, holdTime / maxHoldTime); // Escala proporcional ao tempo segurado
            arrow.localScale = defaultScale * scaleMultiplier; // Escala uniforme com base no multiplicador

            // Alterar a cor da seta de amarelo para vermelho
            Color arrowColor = Color.Lerp(Color.yellow, Color.red, holdTime / maxHoldTime);
            arrowSprite.color = arrowColor;

            // Aplicar tremor leve se a força estiver próxima do máximo
            if (holdTime / maxHoldTime > 0.8f)
            {
                arrow.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            }
            else
            {
                arrow.localPosition = originalPosition;
            }
        }

        // Verificar se a tecla espaço foi liberada
        if (Input.GetKeyUp(KeyCode.Space) && isHoldingSpace)
        {
            // Calcular a direção da seta em relação ao círculo
            Vector2 direction = (arrow.position - circle.position).normalized;

            // Aplicar a força ao círculo na direção da seta
            float force = Mathf.Lerp(minForce, maxForce, holdTime / maxHoldTime);
            circleRb.AddForce(direction * force, ForceMode2D.Impulse);

            // Resetar a escala e a cor da seta para o padrão
            arrow.localScale = defaultScale;
            arrowSprite.color = Color.yellow;

            // Resetar a posição da seta
            arrow.localPosition = originalPosition;

            // Voltar a girar a seta
            isRotating = true;
            isHoldingSpace = false;
        }
    }
}
