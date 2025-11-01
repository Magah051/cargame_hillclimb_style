using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFuel : MonoBehaviour
{
    public Gas gasol;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gasol.AddFuel();
            Destroy(gameObject);
        }
    }
}
