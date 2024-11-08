using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables de referencia privada
    private float horImput; //Referencia al input horizontal del teclado
    private float verImput; //Referencia al input vertical del teclado

    [Header("General References")]
    public Rigidbody playerRb; //Almac�n del Rigidbody del Player. Me permite moverlo.
    public AudioSource playerAudio; //Referencia al reproductor de sonidos del player

    [Header("Movement Variables")]
    public float speed;

    [Header("Jump Variables")]
    public float jumpForce;
    public bool isGrounded;

    [Header("Sound Library")]
    public AudioClip[] soundLibrary; //"Estanter�a" de sonidos del player

    [Header("Respawn Configuration")]
    public GameObject respawnPoint; //Ref al objeto que marca el punto de respawn (transform)
    public float fallLimit; //Valor en -y que al alcanzarlo se ejecutar� el respawn


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Almacenar de manera constante el input de teclado en los ejes X e Y
        horImput = Input.GetAxis("Horizontal");
        verImput = Input.GetAxis("Vertical");
        //Jump();
        if (transform.position.y < fallLimit) { Respawn(); }
    }

    private void FixedUpdate()
    {
        //Aqu� se codea/llama a acciones que dependan de la f�sica CONSTANTE
        VelocityMove();
        //ForceMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            playerAudio.PlayOneShot(soundLibrary[2]);
        }
    }

    void VelocityMove()
    {
        if (isGrounded == true)
        {
            //Movimiento basado en afectar al velocity: "Motor" que imita la capacidad de moverse de un ser vivo
            playerRb.velocity = new Vector3(horImput * speed, playerRb.velocity.y, verImput * speed);
        }
        else
        {
            //Movimiento basado en afectar al velocity: "Motor" que imita la capacidad de moverse de un ser vivo
            playerRb.velocity = new Vector3(horImput * 0, playerRb.velocity.y, verImput * 0);
        }
    }

    void ForceMove()
    {
        //Movimiento basado en aplicar fuerza de empuje en dos ejes: X/Z
        playerRb.AddForce(Vector3.right * horImput * speed);
        playerRb.AddForce(Vector3.forward * verImput * speed);
    }

    //void Jump()
    //{
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        //{
            //playerAudio.PlayOneShot(soundLibrary[0]);
            //isGrounded = false;
            //playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}
        
    //}

    void Respawn()
    {
        playerAudio.PlayOneShot(soundLibrary[1]);
        //Cambia la posici�n del player por la posici�n del punto de respawn
        transform.position = respawnPoint.transform.position;
    }

    public void FreeJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded=false;
        if (isGrounded == false)
        {
            horImput = 0;
            verImput = 0;
        } 
    }
}
