using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeScripts : MonoBehaviour
{
    public void ClassicMode()
    {
        SceneManager.LoadScene("Board");
    }
}
