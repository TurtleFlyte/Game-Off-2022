using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    public TextMeshProUGUI timetext;

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetText(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timetext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
