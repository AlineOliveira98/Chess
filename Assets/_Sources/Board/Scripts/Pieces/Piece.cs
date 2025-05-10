using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private PieceType type;
    [SerializeField] private PieceColor color;
    private Tile currentTile;

    public void SetTile(Tile tile)
    {
        currentTile = tile;
        SetPosition(tile.Position);
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public virtual void Move()
    {

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