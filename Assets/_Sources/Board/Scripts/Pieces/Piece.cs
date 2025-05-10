using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PieceType type;
    [SerializeField] private PieceColor color;
    
    private Tile currentTile;
    protected bool HasMoved;

    public PieceType Type => type;
    public PieceColor Color => color;
    public Vector2Int Coordinate => currentTile.Coord;
    protected int Direction => color == PieceColor.White ? 1 : -1;

    public static Action<Piece, List<Vector2Int>> OnPieceSelected;

    public void SetTile(Tile tile)
    {
        currentTile = tile;
        SetPosition(tile.Position);
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void PieceMovement()
    {
        HasMoved = true;
    }

    public virtual List<Vector2Int> GetPossibleMovements()
    {
        return null;
    }

    protected bool TileIsEmpty(Vector2Int coord)
    {
        var tile = BoardBuilding.Instance.GetTile(coord);

        if(tile == null) return false;
        if(!tile.IsEmpty) return false;

        return true;
    }

    protected bool TileHasOpponentPiece(Vector2Int coord)
    {
        var tile = BoardBuilding.Instance.GetTile(coord);

        if(tile == null) return false;
        if(tile.IsEmpty) return false;
        if(tile.CurrentPiece.Color == color) return false;

        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.PlayersColor == color)
            SelectPiece();
        else
            TryCapturePiece();
    }

    private void TryCapturePiece()
    {
        BoardController.Instance.TryCapturePiece(this);
    }

    private void SelectPiece()
    {
        var moves = GetPossibleMovements();
        OnPieceSelected?.Invoke(this, moves);
    }
}

public enum PieceType
{
    Pawn,

    Rook,

    Knight,

    Bishop,

    Queen,

    King
}

public enum PieceColor
{
    White,
    Black
}