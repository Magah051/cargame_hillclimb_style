using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gas : MonoBehaviour
{
    public Image gasol;
    public Text dialogueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gasol.fillAmount -= Input.GetAxis("Horizontal") / 600f;

        if (gasol.fillAmount == 0)
        {
            //Reiniciando o jogo
            SceneManager.LoadScene(0);
        }
    }

    public void AddFuel()
    {
        gasol.fillAmount += 0.13f;
    }
}
