using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarMovement : MonoBehaviour
{
    public Rigidbody2D carRig;
    public Rigidbody2D backWheelRig;
    public Rigidbody2D frontWheelRig;


    public float speed;
    public float torque;

    private float movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }

    //é utilizado para Física
    private void FixedUpdate()
    {
        backWheelRig.AddTorque(-movement * speed * Time.fixedDeltaTime); //Adiciona força a roda de trás
        frontWheelRig.AddTorque(-movement * speed * Time.fixedDeltaTime);//Adiciona força a roda da frente
        carRig.AddTorque(-movement * torque * Time.fixedDeltaTime);//Adiciona força ao corpo do carro

        if (transform.position.x >= 138f)
        {
            //Reiniciando o jogo
            SceneManager.LoadScene(0);
        }

    }
}
