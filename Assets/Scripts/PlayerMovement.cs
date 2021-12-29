using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cam;

    public float speed = 3f;

    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    Vector3 velocity;
    public float gravity = -9.81f;

    // Update is called once per frame
    void Update()
    {
        
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        velocity.y += gravity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir += velocity;
            float speedFactor = speed * Time.deltaTime;
            characterController.Move(new Vector3(moveDir.x * speedFactor, moveDir.y * Time.deltaTime, moveDir.z * speedFactor));
        }
        else
        {
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}
