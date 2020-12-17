using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class HistoryMenuScript : MonoBehaviour
{
    public void clearHistory()
    {
        string path = Application.persistentDataPath + "/history.chess";
        File.Delete(path);
    }
}
