using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float moveSpeed, acceleration;
    float currentSpeed;
    Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Acceleration and deacceleration
        if (input.magnitude > 0 && currentSpeed >= 0)
        {
            currentSpeed += acceleration * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= acceleration * 2 * moveSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);

        // Movement after acceleration
        rb.velocity = input.normalized * currentSpeed;

        // Set animations
        Animate();
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // Sets animation paramaters
    void Animate()
    {
        if (input != Vector2.zero)
        {
            anim.SetFloat("Horizontal", input.normalized.x);
            anim.SetFloat("Vertical", input.normalized.y);
        }
        anim.SetFloat("Speed", currentSpeed);
    }
}
