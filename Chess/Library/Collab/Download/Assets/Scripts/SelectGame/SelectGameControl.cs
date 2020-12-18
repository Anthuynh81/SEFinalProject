using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SelectGameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GameHistoryControl GameHistory;

    private List<GameObject> buttons = new List<GameObject>();

    List<HistoryData> data = new List<HistoryData>();

    public void CreateList()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        Load();

        foreach (HistoryData game in data)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<SelectGameButton>().SetText(game.titles, game.moveHistory);

            button.transform.SetParent(buttonTemplate.transform.parent, false);

            buttons.Add(button.gameObject);

        }
    }

    public void ClearList()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }

    public void ButtonClicked(List<string> moves)
    {
        GameHistory.LogText(moves);
    }

    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/history.chess";

        if (File.Exists(path))
        {
            FileStream streamOpen = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(streamOpen) as List<HistoryData>;

            Debug.Log("The size of the list is " + data.Count);
            streamOpen.Close();
        }
    }
}
