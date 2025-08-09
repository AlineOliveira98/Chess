using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void UpdatePossibleMovements()
    {
        possibleMovements.Clear();

        Vector2Int[] directions = {
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
        };

        foreach (var dir in directions)
        {
            var pos = Coordinate;
            while (true)
            {
                pos += dir;

                if (!IsInsideBoard(pos)) break;

                if (!TileIsEmpty(pos))
                {
                    if (TileHasOpponentPiece(pos))
                        possibleMovements.Add(pos);

                    break;
                }

                possibleMovements.Add(pos);
            }
        }
    }
}
