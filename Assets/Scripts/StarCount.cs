using UnityEngine;
using UnityEngine.UI;

public class StarCount : MonoBehaviour
{
    // Contador global
    public static int Stars = 0;

    [Header("Referência da UI")]
    [SerializeField] private Text starText; // Texto da UI que mostra o total

    [Header("Configuração")]
    [SerializeField] private string carTag = "Player"; // Tag do carro que coleta

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se quem encostou tem a tag correta
        if (other.CompareTag(carTag))
        {
            Stars++; // soma 1
            AtualizaUI();

            // Destroi o objeto (a estrela)
            Destroy(gameObject);
        }
    }

    private void AtualizaUI()
    {
        if (starText != null)
        {
            starText.text = Stars.ToString();
        }
        else
        {
            Debug.LogWarning("⚠️ Nenhum Text da UI atribuído em StarCount!");
        }
    }
}
