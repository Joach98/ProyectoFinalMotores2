using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_Force_KennySalazar : MonoBehaviour
{
    //Kenny Salazar
    #region variables serializables
    [SerializeField] Rigidbody controller;
    [SerializeField] float force = 0f;
    [SerializeField] float maxSpeed = 0f;
    [SerializeField] float jumpForce = 0f;
    [SerializeField] float stopTimer = 0.3f;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] float velocityMagnitude = 0f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask = default;
    #endregion 
    float timerOriginalValue = 0f;
    bool isGrounded = false;
    void Start()
    {
        //Obtiene el rigidbody
        controller = GetComponent<Rigidbody>();
        //Obtención del valor del contador del editor
        timerOriginalValue = stopTimer;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.AddForce(transform.up * jumpForce);
        }
    }
    private void FixedUpdate()
    {
        //Se aplica el movimiento
        Movement();
        //Control de la magnitud de velocidad, no hace nada más que mostrar el valor en el editor
        velocityMagnitude = controller.velocity.magnitude;
    }
    void Movement()
    {
        //obtiene el input
        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        //Si la velocidad actual es menor que la máxima, se aplica la fuerza
        if (controller.velocity.magnitude < maxSpeed)
        {
            controller.AddForce(transform.forward * x * force);
            controller.AddForce(transform.right * z * force);
        }
        //Si no se recibe ningún input y el jugador se está moviendo
        if((controller.velocity.x > 0 || controller.velocity.z > 0  || controller.velocity.x < 0 || controller.velocity.z < 0) && (x ==0 && z ==0))
        {
            //después de cierto tiempo se detiene completamente
                stopTimer -= Time.deltaTime;
                if (stopTimer <= 0)
                {
                    controller.velocity = new Vector3(0, 0, 0);
                stopTimer = timerOriginalValue;
                }
        }else
        {
            stopTimer = timerOriginalValue ;
        }
    }
}
