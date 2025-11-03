using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [Header("Referências")]
    public GameObject pedal1; // imagem do pedal solto
    public GameObject pedal2; // imagem do pedal pressionado

    [Header("Configuração de Tecla")]
    public KeyCode accelerateKey = KeyCode.D;

    private void Start()
    {
        // Garante que começa com o pedal 1 ativo
        if (pedal1) pedal1.SetActive(true);
        if (pedal2) pedal2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(accelerateKey))
        {
            // Segurando D -> mostra pedal 2
            if (pedal1.activeSelf) pedal1.SetActive(false);
            if (!pedal2.activeSelf) pedal2.SetActive(true);
        }
        else
        {
            // Soltou D -> volta para pedal 1
            if (!pedal1.activeSelf) pedal1.SetActive(true);
            if (pedal2.activeSelf) pedal2.SetActive(false);
        }
    }
}
