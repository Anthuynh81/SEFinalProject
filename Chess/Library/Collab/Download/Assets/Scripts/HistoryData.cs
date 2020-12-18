using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class HistoryData
{
    [SerializeField]
    public string titles;
    [SerializeField]
    public List<string> moveHistory = new List<string>();

    public HistoryData (string title, List<string> moves)
    {
        titles = title;
        moveHistory = moves;
    }

    public string getTitles()
    {
        return titles;
    }

    public List<string> getMoveHistory()
    {
        return moveHistory;
    }
}
