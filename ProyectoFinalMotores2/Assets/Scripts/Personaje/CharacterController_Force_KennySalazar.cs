using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_Force_KennySalazar : MonoBehaviour
{
    [SerializeField] Rigidbody controller;
    [SerializeField] float force = 0f;
    [SerializeField] float Maxforce = 0f;
    [SerializeField] float jumpForce = 0f;
    [SerializeField] float stopTimer = 0.3f;
    [SerializeField] float velocityMagnitude = 0f;

    float timerOriginalValue = 0f;
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        timerOriginalValue = stopTimer;
    }
    private void FixedUpdate()
    {
        Movement();
        velocityMagnitude = controller.velocity.magnitude;
    }
    void Movement()
    {
        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        if (controller.velocity.magnitude < Maxforce)
        {
            controller.AddForce(transform.forward * x * force);
            controller.AddForce(transform.right * z * force);
        }
        if((controller.velocity.x > 0 || controller.velocity.z > 0  || controller.velocity.x < 0 || controller.velocity.z < 0) && (x ==0 && z ==0))
        {
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
        if (Input.GetButtonDown("Jump"))
        {
            controller.AddForce(transform.up * jumpForce);
        }
        Mathf.Clamp(controller.velocity.magnitude, 0f, 10f);
        
    }
}
