using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningMenu : MonoBehaviour
{
    public GameEvent startEvent;

    public void ClickStart()
    {
        startEvent.Raise();
        gameObject.SetActive(false);
    }
}
