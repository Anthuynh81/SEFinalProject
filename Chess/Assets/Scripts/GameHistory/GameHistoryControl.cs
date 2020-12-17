using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryControl : MonoBehaviour
{
    [SerializeField]
    private GameObject textTemplate;

    private List<GameObject> texts = new List<GameObject>();

    public void LogText(List<string> moves)
    {

        if (texts.Count > 0)
        {
            foreach (GameObject text in texts)
            {
                Destroy(text.gameObject);
            }
            texts.Clear();
        }

        //Keeps track of each line
        int isEven = 1;
        
        //Creating Column Headers 'light dark' to signify what moves
        //Came from where.
        string line = "Blue Red";
        GameObject newText = Instantiate(textTemplate) as GameObject;
        newText.SetActive(true);
        newText.GetComponent<GameHistoryText>().SetText(line);
        newText.transform.SetParent(textTemplate.transform.parent, false);
        texts.Add(newText.gameObject);
        line = "";

        //For every move
        foreach (var move in moves)
        {
            //Check if it's even (aka, the end of the line)
            if (isEven % 2 == 0) 
            {
                newText = Instantiate(textTemplate) as GameObject;
                newText.SetActive(true);
                line += move;

                newText.GetComponent<GameHistoryText>().SetText(line);
                line = "";
                newText.transform.SetParent(textTemplate.transform.parent, false);

                texts.Add(newText.gameObject);
                ++isEven;
            }
            //If odd, just add to string to be added to line once another
            //move is completed
            else
            {
                //Add move to line, add a space, and change tracker for even and odd.
                line += move;
                line += " ";
                ++isEven;
            }
        }
        
        //Checking for odd number of moves for history
        if (line != "") 
        {
            newText = Instantiate(textTemplate) as GameObject;
            newText.SetActive(true);
            newText.GetComponent<GameHistoryText>().SetText(line);
            newText.transform.SetParent(textTemplate.transform.parent, false);
            texts.Add(newText.gameObject);
        }
    }

    public void ClearText()
    {
        if (texts.Count > 0)
        {
            foreach (GameObject text in texts)
            {
                Destroy(text.gameObject);
            }
            texts.Clear();
        }
    }
}
