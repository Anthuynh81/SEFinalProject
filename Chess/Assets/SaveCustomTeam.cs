using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveCustomTeam : MonoBehaviour
{
    [SerializeField]
    public string fileName;

    [SerializeField]
    public Dropdown B1;
    [SerializeField]
    public Dropdown B2;
    [SerializeField]
    public Dropdown B3;
    [SerializeField]
    public Dropdown B4;
    [SerializeField]
    public Dropdown B5;
    [SerializeField]
    public Dropdown B6;
    [SerializeField]
    public Dropdown B7;
    [SerializeField]
    public Dropdown B8;
    [SerializeField]
    public Dropdown A1;
    [SerializeField]
    public Dropdown A2;
    [SerializeField]
    public Dropdown A3;
    [SerializeField]
    public Dropdown A4;
    [SerializeField]
    public Dropdown A5;
    [SerializeField]
    public Dropdown A6;
    [SerializeField]
    public Dropdown A7;
    [SerializeField]
    public Dropdown A8;

    public void SavePieces()
    {
        string[] pieces = { B1.options[B1.value].text, B2.options[B2.value].text, B3.options[B3.value].text, B4.options[B4.value].text,
                            B5.options[B5.value].text, B6.options[B6.value].text, B7.options[B7.value].text, B8.options[B8.value].text,
                            A1.options[A1.value].text, A2.options[A2.value].text, A3.options[A3.value].text, A4.options[A4.value].text,
                            A5.options[A5.value].text, A6.options[A6.value].text, A7.options[A7.value].text, A8.options[A8.value].text};

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".chess";

        
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, pieces);
        stream.Close();        
    }
}
