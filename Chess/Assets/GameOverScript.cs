using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textObject;
    public bool isActive;

    public GameObject mGameOver;
    public TextMeshProUGUI Yes;
    public TextMeshProUGUI No;
    public TextMeshProUGUI saved;

    public List<string> moves;

    public void SetText(string myText, List<string> move)
    {
        textObject.enabled = true;
        Yes.enabled = true;
        No.enabled = true;
        saved.enabled = false;


        moves = new List<string>();
        textObject.text = "Checkmate! " + myText + " has won. Would you like to save your game?";
        moves = move;
        isActive = true;
    }

    void Update()
    {
        //If user hits "esc" and wants to return to main menu
        if (isActive && Input.GetKeyDown("y"))
        {
            StartCoroutine(SaveGame("Game has been saved", 1));
            isActive = false;
            
        }
        else if (isActive && Input.GetKeyDown("n"))
        {
            isActive = false;
            mGameOver.SetActive(isActive);
        }
    }

    IEnumerator SaveGame(string message, float delay)
    {
        textObject.enabled = false;
        Yes.enabled = false;
        No.enabled = false;
        saved.enabled = true;
        Save();
        saved.text = "Your game has been saved!";

        yield return new WaitForSeconds(delay);

        mGameOver.SetActive(isActive);
    }

    public void Save()
    {
        string title = DateTime.Now.ToString();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/history.chess";

        List<HistoryData> data = new List<HistoryData>();

        if(File.Exists(path))
        {
            FileStream streamOpen = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(streamOpen) as List<HistoryData>;
            streamOpen.Close();

            FileStream streamClose = new FileStream(path, FileMode.Create);
            HistoryData game = new HistoryData(title, moves);
            data.Add(game);

            formatter.Serialize(streamClose, data);
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            HistoryData game = new HistoryData(title, moves);

            data.Add(game);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
}
