using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [Header("Entrada")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("UI de Pause")]
    [SerializeField] private CanvasGroup pauseUI;   // Panel full-screen com Image preto + CanvasGroup
    [SerializeField] private Button pauseButton;    // <- arraste seu botão Pause aqui (opcional)
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnButton;
    [Range(0f, 1f)][SerializeField] private float dimAlpha = 0.6f;

    private bool isPaused = false;

    private void Awake()
    {
        // Garante EventSystem na cena
        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        // Liga listeners se referência existir
        if (pauseButton) pauseButton.onClick.AddListener(Pause);
        if (resumeButton) resumeButton.onClick.AddListener(Resume);
        if (returnButton) returnButton.onClick.AddListener(ReturnToMenu);

        // Ajusta opacidade base do fundo (Image preto no mesmo GO do CanvasGroup)
        if (pauseUI)
        {
            var img = pauseUI.GetComponent<Image>();
            if (img)
            {
                var c = img.color;
                c.a = dimAlpha; // alpha base quando visível
                img.color = c;
            }
        }
    }

    private void Start()
    {
        ApplyPause(false); // começa despausado
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            TogglePause();
    }

    // USE ESTE MÉTODO no OnClick do botão Pause, se não usar a referência pauseButton:
    public void OnPauseButton()
    {
        Pause();
    }

    public void TogglePause() => ApplyPause(!isPaused);
    public void Pause()
    {
        Debug.Log("[GameController] Pause clicado");
        ApplyPause(true);
    }
    public void Resume()
    {
        Debug.Log("[GameController] Resume clicado");
        ApplyPause(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu");
    }

    private void ApplyPause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = pause ? 0f : 1f;
        AudioListener.pause = pause;

        if (pauseUI)
        {
            pauseUI.alpha = pause ? 1f : 0f;          // mostra/oculta
            pauseUI.blocksRaycasts = pause;           // captura cliques só quando visível
            pauseUI.interactable = pause;
        }
    }
}
