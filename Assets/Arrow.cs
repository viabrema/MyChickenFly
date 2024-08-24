using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Animator animator; // Referência ao Animator do círculo
    private Rigidbody2D circleRb; // Referência ao Rigidbody2D do círculo
    private Vector3 originalPosition; // Posição original para o tremor
    public float flySpeedThreshold = 2f; // Velocidade mínima para ativar a animação de voo
    public ParticleSystem feathers; // Referência ao sistema de partículas das penas
    public Camera mainCamera; // Referência à câmera principal
    public float cameraShakeDuration = 0.2f; // Duração do tremor da câmera
    public float cameraShakeMagnitude = 0.1f; // Magnitude do tremor da câmera
    // public bool cameraFollow = false;
    public bool die = false;
    bool dieFinish = false;

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

        // Obter a referência à câmera principal
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void resetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator dieChicken()
    {
        animator.Play("ChickenDie");
        arrow.gameObject.SetActive(false);
        feathers.Play();
        dieFinish = true;
        circle.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        resetScene();

    }

    void Update()
    {
        if (dieFinish)
        {
            return;
        }
        transform.position = circle.position;
        feathers.transform.position = circle.position;

        float speed = circleRb.velocity.magnitude;

        if (die)
        {
            StartCoroutine(dieChicken());
        }
        else
        {
            if (speed > flySpeedThreshold)
            {
                animator.Play("ChickenFly");
            }
            else
            {
                animator.Play("ChickenIdle");
            }
        }


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
            // Posicionar e ativar o sistema de partículas das penas
            feathers.Play();
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
