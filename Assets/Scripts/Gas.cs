using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    public Image gasol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gasol.fillAmount -= Input.GetAxis("Horizontal") / 700f;
    }

    public void AddFuel()
    {
        gasol.fillAmount += 0.13f;
    }
}
