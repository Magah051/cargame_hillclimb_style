using System.Collections.Generic;
using UnityEngine;

public class BodyCar : MonoBehaviour
{
    [System.Serializable]
    public class CarOption
    {
        public string id;       // mesmo ID usado no seletor
        public Sprite sprite;   // sprite correspondente
    }

    [Header("Opções de Carros")]
    [Tooltip("Associe cada ID do seletor ao sprite correspondente.")]
    public List<CarOption> carOptions = new List<CarOption>();

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("BodyCar: Nenhum SpriteRenderer encontrado neste GameObject!");
            return;
        }

        // Pega o ID salvo pelo seletor de carros
        string selectedId = CarSelectorUI.ObterCarroSelecionadoID();
        Debug.Log("Selected car ID: " + selectedId);

        // Procura o sprite correspondente
        CarOption selectedCar = carOptions.Find(c => c.id == selectedId);

        if (selectedCar != null)
        {
            spriteRenderer.sprite = selectedCar.sprite;
            Debug.Log("BodyCar: Sprite aplicado com sucesso para o ID " + selectedCar.id);
        }
        else
        {
            Debug.LogWarning("BodyCar: Nenhum sprite encontrado para o ID " + selectedId);
        }
    }
}
