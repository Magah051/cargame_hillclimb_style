using UnityEngine;

public class RotacionarPowerUp : MonoBehaviour
{
    [Header("Velocidade de Rotação")]
    [Tooltip("Define a velocidade da rotação no eixo X.")]
    public float velocidadeRotacao = 100f;

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
    }
}
