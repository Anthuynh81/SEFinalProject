using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board mBoard;
    public PieceManager mPieceManager;
    public bool inMenu = false;
    public bool returnToMainMenu = false;
    public GameObject settingsMenu = null;


    void Start()
    {
        // Create the board
        mBoard.Create();

        // Create pieces
        mPieceManager.Setup(mBoard);
    }

    void Update()
    {
        //If user hits "esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = !inMenu;
        }

        //If user hits "esc" and wants to return to main menu
        if (inMenu && Input.GetKeyDown("y"))
        {
            returnToMainMenu = !returnToMainMenu;
        }
        else if (inMenu && Input.GetKeyDown("n"))
        {
            inMenu = !inMenu;
        }
        settingsMenu.SetActive(inMenu);

        //Load main menu scene if user says yes
        if(returnToMainMenu)
        {
            SceneManager.LoadScene(0);
        }
    }
}
