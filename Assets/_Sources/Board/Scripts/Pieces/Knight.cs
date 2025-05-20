using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> GetPossibleMovements()
    {
        var possibleMoves = new List<Vector2Int>();

        Vector2Int[] directions = {
            new Vector2Int(2, 1),
            new Vector2Int(2, -1),
            new Vector2Int(-2, 1),
            new Vector2Int(-2, -1),
            new Vector2Int(1, 2),
            new Vector2Int(-1, 2),
            new Vector2Int(1, -2),
            new Vector2Int(-1, -2),
        };

        foreach (var dir in directions)
        {
            var pos = Coordinate + dir;

            if (!IsInsideBoard(pos)) continue;

            if (TileIsEmpty(pos) || TileHasOpponentPiece(pos))
                possibleMoves.Add(pos);
        }

        return possibleMoves;
    }
}
