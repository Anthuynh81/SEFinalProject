using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;
    [SerializeField]
    private SelectGameControl selectControl;

    List<string> moves = new List<string>();

    public void SetText(string textString, List<string> moveHistory)
    {
        myText.text = textString;
        moves = moveHistory;
    }

    public void onClick()
    {
        foreach (var move in moves)
        {
            Debug.Log(move);
        }
        selectControl.ButtonClicked(moves);
    }
}
