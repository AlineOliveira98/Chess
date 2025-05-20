using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2Int> GetPossibleMovements()
    {
        var possibleMoves = new List<Vector2Int>();
        
        Vector2Int[] directions = {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
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
