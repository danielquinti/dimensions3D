using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI display;
    string message;
    public void SetInfo(string current)
    {
        display.text = current;
    }
}
