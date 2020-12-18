using System;
using System.Collections.Generic;
using UnityEngine;
//using static System.Random;
using Random=UnityEngine.Random;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class PieceManager : MonoBehaviour
{
    [HideInInspector]
    public bool mIsKingAlive = true;

    public bool newGame = false;

    public GameObject mGameOver;

    public GameObject mPiecePrefab;

    private List<BasePiece> mWhitePieces = null;
    private List<BasePiece> mBlackPieces = null;
    private List<BasePiece> mPromotedPieces = new List<BasePiece>();

    public string piece;
    private List<string> moves;

    private string[] mPieceOrder = new string[16]
    {
        "P", "P", "P", "P", "P", "P", "P", "P",
        "R", "KN", "B", "Q", "K", "B", "KN", "R"
    };

    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P",  typeof(Pawn)},
        {"R",  typeof(Rook)},
        {"KN", typeof(Knight)},
        {"B",  typeof(Bishop)},
        {"K",  typeof(King)},
        {"Q",  typeof(Queen)},
        {"BR", typeof(Baron)},
        {"E", typeof(Esquier)}
    };

    private Dictionary<string, Type> mCustomPieceLibrary = new Dictionary<string, Type>()
    {
        {"Pawn",  typeof(Pawn)},
        {"Rook",  typeof(Rook)},
        {"Knight", typeof(Knight)},
        {"Bishop",  typeof(Bishop)},
        {"King",  typeof(King)},
        {"Queen",  typeof(Queen)},
        {"Baron", typeof(Baron)},
        {"Esquier", typeof(Esquier)}
    };

    public void Setup(Board board, string GameMode)
    {
        if ((String.Compare(GameMode, "random")) == 0)
        {
            // Create white pieces
            mWhitePieces = CreateRandomPieces(Color.white, new Color32(80, 124, 159, 255));

            // Create black pieces
            mBlackPieces = CreateRandomPieces(Color.black, new Color32(210, 95, 64, 255));

            // Place pieces
            PlacePieces(1, 0, mWhitePieces, board);
            PlacePieces(6, 7, mBlackPieces, board);

            // White goes first
            SwitchSides(Color.black);
        }
        else if(((String.Compare(GameMode, "classic")) == 0))
        {
            // Create white pieces
            mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255));

            // Create black pieces
            mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255));

            // Place pieces
            PlacePieces(1, 0, mWhitePieces, board);
            PlacePieces(6, 7, mBlackPieces, board);

            // White goes first
            SwitchSides(Color.black);
        }
        else if (((String.Compare(GameMode, "custom")) == 0))
        {
            // Create white pieces
            mWhitePieces = CreateCustomPieces(Color.white, new Color32(80, 124, 159, 255), piece);

            // Create black pieces
            mBlackPieces = CreateCustomPieces(Color.black, new Color32(210, 95, 64, 255), piece);

            // Place pieces
            PlacePieces(1, 0, mWhitePieces, board);
            PlacePieces(6, 7, mBlackPieces, board);

            // White goes first
            SwitchSides(Color.black);
        }
        moves = new List<string>();
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < mPieceOrder.Length; i++)
        {
            // Get the type
            string key = mPieceOrder[i];
            Type pieceType = mPieceLibrary[key];

            // Create
            BasePiece newPiece = CreatePiece(pieceType);
            newPieces.Add(newPiece);

            // Setup
            newPiece.Setup(teamColor, spriteColor, this);
        }

        return newPieces;
    }
    private List<BasePiece> CreateCustomPieces(Color teamColor, Color32 spriteColor, string piece)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        string redPath = Application.persistentDataPath + "/Red.chess";
        string bluePath = Application.persistentDataPath + "/Blue.chess";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream streamRed = new FileStream(redPath, FileMode.Open);
        string[] mRedPieceOrder = formatter.Deserialize(streamRed) as string[];
        streamRed.Close();

        FileStream streamBlue = new FileStream(bluePath, FileMode.Open);
        string[] mBluePieceOrder = formatter.Deserialize(streamBlue) as string[];
        streamBlue.Close();

        if(teamColor == Color.black)
        {
            for (int i = 0; i < mBluePieceOrder.Length; i++)
                    {
                        // Get the type
                        string key = mBluePieceOrder[i];
                        Type pieceType = mCustomPieceLibrary[key];

                        // Create
                        BasePiece newPiece = CreatePiece(pieceType);
                        newPieces.Add(newPiece);

                        // Setup
                        newPiece.Setup(teamColor, spriteColor, this);
                    }
        }else if(teamColor == Color.white)
        {
            for (int i = 0; i < mRedPieceOrder.Length; i++)
            {
                // Get the type
                string key = mRedPieceOrder[i];
                Type pieceType = mCustomPieceLibrary[key];

                // Create
                BasePiece newPiece = CreatePiece(pieceType);
                newPieces.Add(newPiece);

                // Setup
                newPiece.Setup(teamColor, spriteColor, this);
            }
        }
        
        return newPieces;
    }

    private List<BasePiece> CreateRandomPieces(Color teamColor, Color32 spriteColor)
    {
        List<BasePiece> newPieces = new List<BasePiece>();
        //Random rnd = new Random();
        for (int i = 0; i < 8; i++)
        {
            // Get the type
            string key = mPieceOrder[i];
            Type pieceType = mPieceLibrary[key];

            // Create
            BasePiece newPiece = CreatePiece(pieceType);
            newPieces.Add(newPiece);

            // Setup
            newPiece.Setup(teamColor, spriteColor, this);


        }
        List<int> array1 = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            int ran = Random.Range(8, 16);

            bool containsNum = array1.Contains(ran);
            while (containsNum && array1.Count < 9){
                ran = Random.Range(8, 16);
                containsNum = array1.Contains(ran);
            }

            array1.Add(ran);
                // Get the type
                string key = mPieceOrder[ran];
                Type pieceType = mPieceLibrary[key];

                // Create
                BasePiece newPiece = CreatePiece(pieceType);
                newPieces.Add(newPiece);

                // Setup
                newPiece.Setup(teamColor, spriteColor, this);

        }
        return newPieces;
    }

    private BasePiece CreatePiece(Type pieceType)
    {
        // Create new object
        GameObject newPieceObject = Instantiate(mPiecePrefab);
        newPieceObject.transform.SetParent(transform);

        // Set scale and position
        newPieceObject.transform.localScale = new Vector3(1, 1, 1);
        newPieceObject.transform.localRotation = Quaternion.identity;

        // Store new piece
        BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);

        return newPiece;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board)
    {
        for (int i = 0; i < 8; i++)
        {
            // Place pawns    
            pieces[i].Place(board.mAllCells[i, pawnRow]);

            // Place royalty
            pieces[i + 8].Place(board.mAllCells[i, royaltyRow]);
        }
    }

    private void SetInteractive(List<BasePiece> allPieces, bool value)
    {
        foreach (BasePiece piece in allPieces)
            piece.enabled = value;
    }

    public void SwitchSides(Color color)
    {
        bool isBlackTurn = color == Color.white ? true : false;

        if (!mIsKingAlive)
        {
            mGameOver.SetActive(true);
            mGameOver.GetComponent<GameOverScript>().SetText(isBlackTurn ? "Black" : "White", moves);

            // Reset pieces
            ResetPieces();

            // King has risen from the dead
            mIsKingAlive = true;

            // Change color to black, so white can go first again
            color = Color.black;
        }

        isBlackTurn = color == Color.white ? true : false;

        // Set team interactivity
        SetInteractive(mWhitePieces, !isBlackTurn);

        // Disable this so player can't move pieces
        SetInteractive(mBlackPieces, isBlackTurn);

        // Set promoted interactivity
        foreach (BasePiece piece in mPromotedPieces)
        {
            bool isBlackPiece = piece.mColor != Color.white ? true : false;
            bool isPartOfTeam = isBlackPiece == true ? isBlackTurn : !isBlackTurn;

            piece.enabled = isPartOfTeam;
        }
    }

    public void ResetPieces()
    {
        foreach (BasePiece piece in mPromotedPieces)
        {
            piece.Kill();
            Destroy(piece.gameObject);
        }

        mPromotedPieces.Clear();

        foreach (BasePiece piece in mWhitePieces)
            piece.Reset();

        foreach (BasePiece piece in mBlackPieces)
            piece.Reset();
    }

    public void PromotePiece(Pawn pawn, Cell cell, Color teamColor, Color spriteColor)
    {
        // Kill Pawn
        pawn.Kill();

        // Create
        BasePiece promotedPiece = CreatePiece(typeof(Queen));
        promotedPiece.Setup(teamColor, spriteColor, this);

        // Place piece
        promotedPiece.Place(cell);

        // Add
        mPromotedPieces.Add(promotedPiece);
    }

    public void PromotePiece(Esquier esquier, Cell cell, Color teamColor, Color spriteColor)
    {
        // Kill Pawn
        esquier.Kill();

        // Create
        BasePiece promotedPiece = CreatePiece(typeof(Queen));
        promotedPiece.Setup(teamColor, spriteColor, this);

        // Place piece
        promotedPiece.Place(cell);

        // Add
        mPromotedPieces.Add(promotedPiece);
    }

    public void addMove(string piece, Vector2Int position)
    {
        char letter = (char)(position.x + 65);
        int num = position.y + 1;
        string move;
        switch (piece)
        {
            case "Pawn":
                move = "P." + letter + num;
                break;
            case "Rook":
                move = "R." + letter + num;
                break;
            case "Bishop":
                move = "B." + letter + num;
                break;
            case "Knight":
                move = "KN." + letter + num;
                break;
            case "Queen":
                move = "Q." + letter + num;
                break;
            case "King":
                move = "K." + letter + num;
                break;
            case "Esquier":
                move = "E." + letter + num;
                break;
            case "Baron":
                move = "Ba." + letter + num;
                break;
            default:
                move = "error";
                break;
        }

        moves.Add(move);
    }
}
