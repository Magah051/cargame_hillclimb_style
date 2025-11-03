using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [Header("Referências")]
    public Rigidbody2D carRb;
    public Text speedText;
    public Image needle;

    [Header("Configuração")]
    public float maxSpeed = 120f;          // velocidade máxima (km/h)
    public float minRotation = 0f;         // ângulo da agulha parada
    public float maxRotation = -220f;      // ângulo da agulha no máximo

    private float currentRotation;

    private void Update()
    {
        if (carRb == null) return;

        // velocidade atual em km/h
        float speed = carRb.velocity.magnitude * 3.6f;

        // texto
        if (speedText)
            speedText.text = Mathf.RoundToInt(speed*5f) + " km/h";

        // calcula rotação proporcional
        if (needle)
        {
            float normalized = Mathf.Clamp01(speed / maxSpeed);
            float targetRotation = Mathf.Lerp(minRotation, maxRotation, normalized);

            // suaviza o movimento (interpolação)
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 10f);
            needle.rectTransform.localEulerAngles = new Vector3(0, 0, currentRotation);
        }
    }
}
