using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pawn : Piece
{
    public override List<Vector2Int> GetPossibleMovements()
    {
        var possibleMoves = new List<Vector2Int>();

        var fowardMove = new Vector2Int(Coordinate.x, Coordinate.y + Direction);

        if(TileIsEmpty(fowardMove))
        {
            possibleMoves.Add(fowardMove);

            if(!HasMoved)
            {
                var doubleFowardMove = new Vector2Int(Coordinate.x, Coordinate.y + Direction * 2);

                if(TileIsEmpty(doubleFowardMove))
                    possibleMoves.Add(doubleFowardMove);
            }
        }

        var captureCoords = new List<Vector2Int>()
        {
            new Vector2Int(Coordinate.x-1, Coordinate.y + Direction),
            new Vector2Int(Coordinate.x+1, Coordinate.y + Direction)
        };

        foreach (var coord in captureCoords)
        {
            if(TileHasOpponentPiece(coord))
            {
                possibleMoves.Add(coord);
            }
        }

        return possibleMoves;
    }
}
