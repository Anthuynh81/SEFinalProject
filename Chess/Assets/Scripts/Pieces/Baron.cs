using UnityEngine;
using UnityEngine.UI;

public class Baron : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Queen stuff
        mMovement = new Vector3Int(2, 2, 2);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Baron");
    }
}
