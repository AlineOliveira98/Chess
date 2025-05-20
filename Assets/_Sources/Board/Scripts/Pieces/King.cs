using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> GetPossibleMovements()
    {
        //O rei nao pode se mover para uma casa onde ele possa ser atacado
        //também nao pode se mover para uma casa onde o rei do oponente possa se mover

        var possibleMoves = new List<Vector2Int>();

        Vector2Int[] directions = {
            Vector2Int.right,
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.down
        };

        foreach (var dir in directions)
        {

        }

        return possibleMoves;
    }
}
