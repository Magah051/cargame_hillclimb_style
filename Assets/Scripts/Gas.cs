using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gas : MonoBehaviour
{
    public Image gasol;
    public float consumptionFuel = 600f;
    public float fuelPowerUp = 0.13f;
    public float consumptionMuml = 100f;

    private void Update()
    {
        // reduz combustível somente se estiver acelerando
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gasol.fillAmount -= 1f / consumptionFuel * Time.deltaTime * consumptionMuml;
        }

        if (gasol.fillAmount <= 0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddFuel()
    {
        gasol.fillAmount += fuelPowerUp;
    }
}
