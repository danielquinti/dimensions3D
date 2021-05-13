using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * This class mediates between the other scripts and the TextMesh 
 * graphic components to change the text that they display
 */
public class InfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI display;
    string message;
    public void SetInfo(string current)
    {
        display.text = current;
    }
}
