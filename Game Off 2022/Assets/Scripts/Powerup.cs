using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public Sprite sprite;
    [TextArea] public string description;
    public float price;
    public float maxBuyAmount;
    public TextMeshProUGUI priceText, descriptionText;
    public GameObject soldOutSprite;

    public bool soda, coupon, coffee;

    private void Start()
    {
        priceText.text = price.ToString() + "s";
        descriptionText.text = description;
    }

    public void CannotBuy()
    {
        soldOutSprite.SetActive(true);

        GetComponent<Button>().enabled = false;
    }
}
