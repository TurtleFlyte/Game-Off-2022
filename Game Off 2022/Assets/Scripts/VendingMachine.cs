using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public ScriptableObject[] powerups;

    public Canvas ui;

    public void Interact()
    {
        ui.enabled = true;
    }
}
