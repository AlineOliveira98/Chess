using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override void UpdatePossibleMovements()
    {
        //Fazer roque
        possibleMovements.Clear();

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
                        possibleMovements.Add(pos);

                    break;
                }

                possibleMovements.Add(pos);
            }
        }
    }
}
