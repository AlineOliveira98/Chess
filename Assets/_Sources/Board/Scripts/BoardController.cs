using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;

    private Piece selectedPiece;
    private List<Vector2Int> highlightedTiles = new();
    
    public static Action OnBoardReseted;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void OnEnable()
    {
        Piece.OnPieceSelected += SelectPiece;
    }

    void OnDisable()
    {
        Piece.OnPieceSelected -= SelectPiece;
    }

    private void SelectPiece(Piece piece, List<Vector2Int> moves)
    {
        if(selectedPiece == piece)
        {
            selectedPiece = null;
            ResetNormalColorTiles();
            return;
        }

        selectedPiece = piece;
        ResetNormalColorTiles();
        HailightTiles(moves);
    }

    public bool TryCapturePiece(Piece piece)
    {
        if (!highlightedTiles.Contains(piece.Coordinate)) return false;

        var tile = BoardBuilding.Instance.GetTile(piece.Coordinate);
        tile.CapturePiece();

        MovePiece(tile);
        return true;
    }

    private void ResetBoard()
    {
        OnBoardReseted?.Invoke();
    }

    public void MovePiece(Tile tileSelected)
    {
        if (selectedPiece == null) return;
        if (!highlightedTiles.Contains(tileSelected.Coord)) return;

        selectedPiece.CurrentTile.RemovePiece();
        selectedPiece.PieceMovement();
        tileSelected.SetPiece(selectedPiece);

        selectedPiece = null;
        ResetNormalColorTiles();
    }

    private void ResetNormalColorTiles()
    {
        foreach (var tileCoords in highlightedTiles)
        {
            BoardBuilding.Instance.GetTile(tileCoords).SetHighLight();
        }

        highlightedTiles.Clear();
    }

    public void HailightTiles(List<Vector2Int> coords)
    {
        highlightedTiles = coords;

        foreach (var coord in coords)
        {
            BoardBuilding.Instance.GetTile(coord).SetHighLight(true);
        }
    }
}
