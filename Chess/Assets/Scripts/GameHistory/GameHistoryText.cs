using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHistoryText : MonoBehaviour
{
    public void SetText(string myText)
    {
        GetComponent<Text>().text = myText;
    }
}
