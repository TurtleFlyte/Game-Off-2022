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

    public GameEvent depositeEvent;
    public GameEvent interactVendingMachine;

    // powerup parameters
    float walkSpeedMultiplier = 1;

    bool canMove = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            // Acceleration and deacceleration
            if (input.magnitude > 0 && currentSpeed >= 0)
            {
                currentSpeed += acceleration * moveSpeed * Time.deltaTime * walkSpeedMultiplier;
            }
            else
            {
                currentSpeed -= acceleration * 2 * moveSpeed * Time.deltaTime * walkSpeedMultiplier;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed * walkSpeedMultiplier);

            // Movement after acceleration
            rb.velocity = input.normalized * currentSpeed;

            // Set animations
            Animate();
        }
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

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Checks all colliders around it
            RaycastHit2D[] area = Physics2D.CircleCastAll(transform.position, pickupDistance, Vector2.zero);

            // Pickup and deposit items
            if (!hasItem)
            {
                // Checks all the colliders around it for the pickup area then takes the item and sets the pickup area item to null
                PickupArea pickup;

                for (int i = area.Length - 1; i > 0; i--)
                {
                    if (area[i].collider.gameObject.name == "Pickup Area")
                    {
                        pickup = area[i].collider.gameObject.GetComponent<PickupArea>();

                        currentItem = pickup.currentItem;
                        pickup.currentItem = null;
                        Destroy(pickup.instansiatedItem);
                        hasItem = true;
                    }
                }
            }
            else
            {
                // Checks all the colliders around it for the area the item is supposed to go to then calls the deposit event if it is.

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

            // interact with vending machine
            for (int j = area.Length - 1; j > 0; j--)
            {
                if (area[j].collider.gameObject.name == "Vending Machine")
                {
                    interactVendingMachine.Raise();
                }
            }
        }
    }

    public float WalkSpeedMultiplier
    {
        get { return walkSpeedMultiplier; }
        set { walkSpeedMultiplier = value; }
    }

    public bool CanMove
    {
        set { canMove = value; }
    }
}
