using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupArea : MonoBehaviour
{
    public GameObject[] items;
    public Transform itemSpot;

    [HideInInspector] public GameObject currentItem;
    [HideInInspector] public GameObject instansiatedItem;

    void Start()
    {
        currentItem = items[Random.Range(0, items.Length - 1)];
        instansiatedItem = Instantiate(currentItem, itemSpot);
    }

    private void Update()
    {
        
    }

    public void NewPickup()
    {
        Debug.Log("new pickup");
        currentItem = items[Random.Range(0, items.Length - 1)];
        instansiatedItem = Instantiate(currentItem, itemSpot);
    }
}
