using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}
public class Board : MonoBehaviour
{
    public GameObject mCellPrefab;

    [HideInInspector]
    public Cell[,] mAllCells = new Cell[8, 8];

    //Board is created here
    public void Create()
    {
        //2x for loops to fill the board
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //Creating a new cell
                GameObject newCell = Instantiate(mCellPrefab, transform);

                //Setting position
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((i * 100) + 200, (j * 100));

                //Actual setup
                mAllCells[i, j] = newCell.GetComponent<Cell>();
                mAllCells[i, j].Setup(new Vector2Int(i, j), this);
            }
        }

        //Now we create 'checkerboard' pattern
        for (int i = 0; i < 8; i += 2)
        {
            for (int j = 0; j < 8; j++)
            {
                //Creating the offset for every other line
                int offset = (j % 2 != 0) ? 0 : 1; //Checking if the line is even
                int finalX = i + offset; //If the value is odd, we need to add an offset

                //Coloring
                mAllCells[finalX, j].GetComponent<Image>().color = new Color32(230, 220, 197, 255);
            }
        }
    }

    // New
    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {
        // Bounds check
        if (targetX < 0 || targetX > 7)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > 7)
            return CellState.OutOfBounds;

        // Get cell
        Cell targetCell = mAllCells[targetX, targetY];

        // If the cell has a piece
        if (targetCell.mCurrentPiece != null)
        {
            // If friendly
            if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                return CellState.Friendly;

            // If enemy
            if (checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                return CellState.Enemy;
        }

        return CellState.Free;
    }
}
