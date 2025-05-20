using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector2Int> GetPossibleMovements()
    {
        var possibleMoves = new List<Vector2Int>();

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
                        possibleMoves.Add(pos);

                    break;
                }

                possibleMoves.Add(pos);
            }
        }

        return possibleMoves;
    }
}
