using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionMenuScript : MonoBehaviour
{
    public void CustomMode()
    {
        SceneManager.LoadScene("custom");
    }
}
