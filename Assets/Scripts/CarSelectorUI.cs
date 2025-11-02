using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Se usar TextMeshPro, troque "using UnityEngine.UI;" por "using TMPro;"
// e mude o tipo de nomeCarroText para TextMeshProUGUI.

public class CarSelectorUI : MonoBehaviour
{
    [System.Serializable]
    public class CarOption
    {
        public string id = "carro_01";
        public string nomeExibicao = "Carro A";
        public Sprite imagem;
    }

    [Header("Opções de Carro")]
    public List<CarOption> carros = new List<CarOption>();

    [Header("Referências de UI")]
    public Image imagemCarro;    // UI > Image com o sprite do carro
    public Text nomeCarroText;   // (ou TMP) para exibir o nome
    public Button btnAnterior;
    public Button btnProximo;
    public Button btnJogar;

    [Header("Cena de Jogo (opcional)")]
    public string nomeCenaJogo = "";

    [Header("Transição")]
    [Tooltip("Duração da transição de saída/entrada.")]
    public float transitionDuration = 0.35f;
    [Tooltip("Deslocamento horizontal durante a transição (pixels).")]
    public float moveOffset = 180f;
    [Tooltip("Curva da transição (0..1).")]
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Idle (efeito em loop)")]
    [Tooltip("Ativa efeito de idle (pulsar/oscilar).")]
    public bool idleAtivo = true;
    [Tooltip("Amplitude do 'pulsar' (scale).")]
    public float idleScaleAmplitude = 0.03f;
    [Tooltip("Velocidade do 'pulsar'.")]
    public float idleScaleSpeed = 2.2f;
    [Tooltip("Amplitude do bob vertical (px).")]
    public float idleBobAmplitude = 6f;
    [Tooltip("Velocidade do bob vertical.")]
    public float idleBobSpeed = 1.5f;

    private int indiceAtual = 0;
    private const string PlayerPrefsKey = "CARRO_SELECIONADO_ID";
    private CanvasGroup cg;
    private RectTransform imgRT;
    private Vector2 baseAnchoredPos;
    private Vector3 baseScale;
    private Coroutine transitionCR;
    private Coroutine idleCR;

    void Awake()
    {
        if (imagemCarro == null)
        {
            Debug.LogError("CarSelectorUI: atribua a Image do carro no Inspector.");
            enabled = false;
            return;
        }

        // Garante CanvasGroup para fade
        cg = imagemCarro.GetComponent<CanvasGroup>();
        if (!cg) cg = imagemCarro.gameObject.AddComponent<CanvasGroup>();

        imgRT = imagemCarro.rectTransform;
        baseAnchoredPos = imgRT.anchoredPosition;
        baseScale = imgRT.localScale;
    }

    void Start()
    {
        if (btnAnterior) btnAnterior.onClick.AddListener(Anterior);
        if (btnProximo) btnProximo.onClick.AddListener(Proximo);
        if (btnJogar) btnJogar.onClick.AddListener(Jogar);

        // Restaura seleção, se existir
        var salvo = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (!string.IsNullOrEmpty(salvo))
        {
            int idx = carros.FindIndex(c => c.id == salvo);
            if (idx >= 0) indiceAtual = idx;
        }

        // Mostra primeiro carro sem transição (setup)
        AplicarCarroNaUI(carros.Count > 0 ? carros[indiceAtual] : null);
        cg.alpha = 1f;
        imgRT.anchoredPosition = baseAnchoredPos;

        // Idle
        if (idleAtivo) idleCR = StartCoroutine(IdleLoop());
    }

    private void AplicarCarroNaUI(CarOption car)
    {
        if (car == null)
        {
            imagemCarro.enabled = false;
            if (nomeCarroText) nomeCarroText.text = "Sem carros configurados";
            if (btnAnterior) btnAnterior.interactable = false;
            if (btnProximo) btnProximo.interactable = false;
            if (btnJogar) btnJogar.interactable = false;
            return;
        }

        imagemCarro.enabled = true;
        imagemCarro.sprite = car.imagem;
        if (nomeCarroText) nomeCarroText.text = car.nomeExibicao;

        bool multi = carros.Count > 1;
        if (btnAnterior) btnAnterior.interactable = multi;
        if (btnProximo) btnProximo.interactable = multi;
        if (btnJogar) btnJogar.interactable = true;
    }

    public void Proximo()
    {
        TrocarCarro(+1);
    }

    public void Anterior()
    {
        TrocarCarro(-1);
    }

    private void TrocarCarro(int direcao) // +1 próximo, -1 anterior
    {
        if (carros == null || carros.Count == 0) return;

        // trava botões durante a transição
        SetButtonsInteractable(false);

        // corrige índice alvo
        int novoIndice = indiceAtual + direcao;
        if (novoIndice < 0) novoIndice = carros.Count - 1;
        if (novoIndice >= carros.Count) novoIndice = 0;

        // cancela transições/idle anteriores
        if (transitionCR != null) StopCoroutine(transitionCR);
        if (idleCR != null)
        {
            StopCoroutine(idleCR);
            idleCR = null;
        }

        transitionCR = StartCoroutine(TransicaoCarro(novoIndice, direcao));
    }

    private IEnumerator TransicaoCarro(int novoIndice, int direcao)
    {
        // 1) SAÍDA (fade + slide)
        float t = 0f;
        Vector2 startPos = baseAnchoredPos;
        Vector2 endPos = baseAnchoredPos + Vector2.left * moveOffset * Mathf.Sign(direcao);
        float startAlpha = 1f, endAlpha = 0f;

        while (t < transitionDuration)
        {
            float k = transitionCurve.Evaluate(t / transitionDuration);
            imgRT.anchoredPosition = Vector2.Lerp(startPos, endPos, k);
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, k);
            t += Time.unscaledDeltaTime; // UI responsiva mesmo com Time.timeScale alterado
            yield return null;
        }
        imgRT.anchoredPosition = endPos;
        cg.alpha = 0f;

        // Troca o conteúdo
        indiceAtual = novoIndice;
        AplicarCarroNaUI(carros[indiceAtual]);

        // 2) ENTRADA (começa deslocado do lado oposto)
        Vector2 inStart = baseAnchoredPos - Vector2.left * moveOffset * Mathf.Sign(direcao);
        Vector2 inEnd = baseAnchoredPos;
        imgRT.anchoredPosition = inStart;

        t = 0f;
        startAlpha = 0f; endAlpha = 1f;
        while (t < transitionDuration)
        {
            float k = transitionCurve.Evaluate(t / transitionDuration);
            imgRT.anchoredPosition = Vector2.Lerp(inStart, inEnd, k);
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, k);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        imgRT.anchoredPosition = baseAnchoredPos;
        cg.alpha = 1f;

        transitionCR = null;

        // reativa idle
        if (idleAtivo) idleCR = StartCoroutine(IdleLoop());

        // libera botões
        SetButtonsInteractable(true);
    }

    private void SetButtonsInteractable(bool v)
    {
        if (btnAnterior) btnAnterior.interactable = v;
        if (btnProximo) btnProximo.interactable = v;
        if (btnJogar) btnJogar.interactable = v && (carros != null && carros.Count > 0);
    }

    private IEnumerator IdleLoop()
    {
        float t = 0f;
        // reseta base
        imgRT.localScale = baseScale;
        imgRT.anchoredPosition = baseAnchoredPos;

        while (true)
        {
            // scale ping-pong
            float s = 1f + Mathf.Sin(t * idleScaleSpeed) * idleScaleAmplitude;
            imgRT.localScale = baseScale * s;

            // bob vertical
            float y = Mathf.Sin(t * idleBobSpeed) * idleBobAmplitude;
            imgRT.anchoredPosition = baseAnchoredPos + new Vector2(0f, y);

            t += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void Jogar()
    {
        if (carros == null || carros.Count == 0) return;
        var escolhido = carros[indiceAtual];
        PlayerPrefs.SetString(PlayerPrefsKey, escolhido.id);
        PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(nomeCenaJogo))
        {
            SceneManager.LoadScene(nomeCenaJogo);
        }
        else
        {
            Debug.Log($"Carro selecionado: {escolhido.id} - {escolhido.nomeExibicao}");
        }
    }

    // Para uso na cena do jogo:
    public static string ObterCarroSelecionadoID()
    {
        return PlayerPrefs.GetString(PlayerPrefsKey, "");
    }
}
