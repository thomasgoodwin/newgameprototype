using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cam;

    public float speed = 3f;
    public float acceleration = 10f;
    public float friction = 0.01f;
    public float stopSpeed = 0.1f;
    public float jumpVelocity = 2f;

    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    Vector3 velocity;
    public float gravity = -9.81f;

    private bool isOnGround = false;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        velocity.y += gravity * Time.deltaTime;

        if (jump > 0f && isOnGround)
        {
            velocity.y = jumpVelocity;
            isOnGround = false;
            Debug.Log("jump");
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 wishDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float alreadySpeed = Vector3.Dot(wishDir, velocity);
            Debug.Log("alreadySpeed:" + alreadySpeed);
            if (speed > alreadySpeed)
            {
                velocity += wishDir * Mathf.Min(speed - alreadySpeed, acceleration * Time.deltaTime * friction);
            }
            
            if (isOnGround)
            {
                velocity.y = 0;
                Friction();
            }

            if (alreadySpeed > speed)
            {
                velocity *= speed / alreadySpeed;
            }

            CollisionFlags flags = characterController.Move(velocity * Time.deltaTime + (isOnGround ? Vector3.down * 0.01f : Vector3.zero));
            isOnGround = flags.HasFlag(CollisionFlags.Below);
        }
        else// if (velocity.magnitude >= stopSpeed)
        {
            if (isOnGround)
            {
                velocity.y = 0;
                Friction();
            }
            CollisionFlags flags = characterController.Move(velocity * Time.deltaTime + (isOnGround ? Vector3.down * 0.01f : Vector3.zero));
            isOnGround = flags.HasFlag(CollisionFlags.Below);
        }
        
    }
    void Friction()
    {
        float curSpeed = velocity.magnitude;
        float control = Mathf.Max(speed, stopSpeed);
        float drop = control * friction * Time.deltaTime;
        float newSpeed = Mathf.Max(0, curSpeed - drop);
        if (newSpeed != curSpeed)
        {
            velocity *= newSpeed / curSpeed;
        }
    }
}
