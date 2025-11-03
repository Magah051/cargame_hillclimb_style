using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Sai do modo Play no editor
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_WEBGL
        // No WebGL não dá pra “fechar o jogo”, mas podemos simular:
        // 1️⃣ Recarregar a página
        Application.ExternalEval("location.reload();");

        // ou 2️⃣ Ir para uma URL (por exemplo, tela inicial ou página externa)
        // Application.OpenURL("https://josemagalhaes.itch.io/urban-race-x");
#else
        // Funciona normalmente em PC/Mobile builds
        Application.Quit();
#endif
    }
}
