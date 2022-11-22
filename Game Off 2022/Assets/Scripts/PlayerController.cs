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

    GameObject currentItem;
    bool hasItem = false;
    public float pickupDistance;

    public OnDepositItemEvent depositeEvent;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

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

    // Sets animation paramaters depending on the player input
    void Animate()
    {
        if (input != Vector2.zero)
        {
            anim.SetFloat("Horizontal", input.normalized.x);
            anim.SetFloat("Vertical", input.normalized.y);
        }
        anim.SetFloat("Speed", currentSpeed);
    }

    public void ChangeItem(InputAction.CallbackContext context)
    {
        if (!hasItem && context.started)
        {
            // Checks all the colliders around it for the pickup area then takes the item and sets the pickup area item to null

            RaycastHit2D[] area = Physics2D.CircleCastAll(transform.position, pickupDistance, Vector2.zero);
            PickupArea pickup;

            for(int i = area.Length-1; i > 0; i--)
            {
                if(area[i].collider.gameObject.name == "Pickup Area")
                {
                    pickup = area[i].collider.gameObject.GetComponent<PickupArea>();

                    currentItem = pickup.currentItem;
                    pickup.currentItem = null;
                    Destroy(pickup.instansiatedItem);
                    hasItem = true;
                }
            }
        }
        else if(context.started)
        {
            // Checks all the colliders around it for the area the item is supposed to go to then calls the deposit event if it is.

            RaycastHit2D[] area = Physics2D.CircleCastAll(transform.position, pickupDistance, Vector2.zero);
            //for (int i = area.Length - 1; i > 0; i--)
            //{
            //    Debug.Log(area[i].collider.gameObject.name);
            //}

            for (int i = area.Length - 1; i > 0; i--)
            {
                if (area[i].collider.gameObject.name == currentItem.name)
                {
                    depositeEvent.Raise();

                    currentItem = null;
                    hasItem = false;

                    return;
                }
            }
        }
    }
}
