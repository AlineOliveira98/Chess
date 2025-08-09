using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override void UpdatePossibleMovements()
    {
        //O rei nao pode se mover para uma casa onde ele possa ser atacado
        //também nao pode se mover para uma casa onde o rei do oponente possa se mover
        //Movimento especial com a torre

        possibleMovements.Clear();

        Vector2Int[] directions = {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down,
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
        };

        foreach (var dir in directions)
        {
            var pos = Coordinate + dir;

            if (TileIsEmpty(pos))
            {
                if (!BoardBuilding.Instance.CheckmatePosition(pos, Color))
                {
                    possibleMovements.Add(pos);
                }
            }
            else if(TileHasOpponentPiece(pos))
            {
                possibleMovements.Add(pos);
            }
        }
    }
}
