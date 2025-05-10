using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;

    private Piece currentPiece;
    
    public Vector2Int Coord {get; private set; }

    public Vector2 Position => transform.position;
    public bool IsEmpty => currentPiece == null;

    void OnEnable()
    {
        BoardManager.OnBoardReseted += Reset;    
    }

    void OnDisable()
    {
        BoardManager.OnBoardReseted -= Reset;
    }

    void Start()
    {
        
    }

    public void Setup(Vector2Int coord)
    {
        Coord = coord;
        gameObject.name = $"Tile {coord.x}-{coord.y}";

        var tileBlackColor = (coord.x + coord.y) % 2 == 0;
        background.color = tileBlackColor ? Color.black : Color.white;
    }

    public void SetPiece(Piece piece)
    {
        currentPiece = piece;
        currentPiece.SetTile(this);
    }

    private void Reset()
    {
         
    }
}