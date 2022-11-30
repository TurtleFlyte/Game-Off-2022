using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class VendingMachineManager : MonoBehaviour
{
    public Canvas ui;
    public GameObject[] powerupCards;

    public TimeManager timeManager;

    float slowDownBought = 0, speedBought = 0;
    bool discountBought;

    public PlayerController player;

    public void Interact()
    {
        Debug.Log("activate vending machine");

        ui.gameObject.SetActive(true);

        timeManager.CanCountDown = false;
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        Powerup powerup = buttonRef.GetComponent<Powerup>();

        if (timeManager.GetTime > powerup.price)
        {
            if (powerup.soda && powerup.maxBuyAmount > slowDownBought)
            {
                timeManager.GetTime -= powerup.price;

                slowDownBought++;

                timeManager.powerupMultiplier = timeManager.powerupMultiplier / 1.5f;

                powerup.price += 10;
                powerup.priceText.text = powerup.price.ToString() + "s";
                Debug.Log("time bought");
            }
            if (powerup.coupon && !discountBought)
            {
                timeManager.GetTime -= powerup.price;

                discountBought = true;

                ChangeDiscount();
                powerup.CannotBuy();
                Debug.Log("discount bought");
            }
            if (powerup.coffee && powerup.maxBuyAmount > slowDownBought)
            {
                timeManager.GetTime -= powerup.price;

                speedBought++;

                player.WalkSpeedMultiplier += 0.05f;

                powerup.price += 10;
                powerup.priceText.text = powerup.price.ToString() + "s";

                Debug.Log("speed bought");
            }
        }
        //else if (timeManager.GetTime > powerup.price / 2 && discountBought)
        //{
        //    if (powerup.soda && powerup.maxBuyAmount < slowDownBought)
        //    {
        //        timeManager.GetTime -= powerup.price / 2;

        //        slowDownBought++;
        //    }
        //    if (powerup.coffee && powerup.maxBuyAmount > slowDownBought)
        //    {
        //        timeManager.GetTime -= powerup.price / 2;

        //        speedBought++;
        //    }
        //}

        if(powerup.soda && powerup.maxBuyAmount == slowDownBought)
        {
            powerup.CannotBuy();
        }
        if (powerup.coffee && powerup.maxBuyAmount == speedBought)
        {
            powerup.CannotBuy();
        }
    }

    public void CloseUI(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ui.gameObject.SetActive(false);

            timeManager.CanCountDown = true;
        }
    }

    void ChangeDiscount()
    {
        for (int i = powerupCards.Length - 1; i >= 0; i--)
        {
            Powerup card = powerupCards[i].GetComponent<Powerup>();

            card.price = card.price / 2;

            card.priceText.text = card.price.ToString() + "s";
        }
    }
}
