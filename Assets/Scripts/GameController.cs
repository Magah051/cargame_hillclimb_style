using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Entrada")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("UI de Pause")]
    [SerializeField] private CanvasGroup pauseUI;   // CanvasGroup do painel de pausa (full screen)
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnButton;
    [Range(0f, 1f)][SerializeField] private float dimAlpha = 0.6f; // quão escuro fica o fundo

    private bool isPaused = false;

    private void Awake()
    {
        // Liga os botões se foram arrastados no Inspector
        if (resumeButton) resumeButton.onClick.AddListener(Resume);
        if (returnButton) returnButton.onClick.AddListener(ReturnToMenu);

        // Garante o nível de escurecimento do fundo (o Panel deve ter um Image preto)
        if (pauseUI)
        {
            var img = pauseUI.GetComponent<Image>();
            if (img)
            {
                var c = img.color;
                c.a = dimAlpha;
                img.color = c;
            }
        }
    }

    private void Start()
    {
        ApplyPause(false); // começa jogável
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
            TogglePause();
    }

    // Pode ser chamado por um botão "Pause"
    public void OnPauseButton()
    {
        Pause();
    }

    public void TogglePause()
    {
        ApplyPause(!isPaused);
    }

    public void Pause()
    {
        ApplyPause(true);
    }

    public void Resume()
    {
        ApplyPause(false);
    }

    public void ReturnToMenu()
    {
        // Sempre restaura o tempo/áudio antes de trocar de cena
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu"); // nome exato da cena do menu
    }

    private void ApplyPause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = pause ? 0f : 1f;
        AudioListener.pause = pause;

        if (pauseUI)
        {
            pauseUI.alpha = pause ? 1f : 0f;
            pauseUI.blocksRaycasts = pause;
            pauseUI.interactable = pause;
        }
    }
}
