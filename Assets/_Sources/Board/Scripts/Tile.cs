using UnityEngine;

[SelectionBaseAttribute]
public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Piece initialPiece;

    private Piece originalPiece;
    private Piece currentPiece;
    private Vector2Int coordinates;
    private BoardManager boardManager;

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

    public void Setup(BoardManager boardManager, Vector2Int coordinates, Vector3 pos, bool isBlackTile)
    {
        this.boardManager = boardManager;
        this.coordinates = coordinates;
        gameObject.name = $"Tile {coordinates.x}-{coordinates.y}";
        transform.position = pos;

        background.color = isBlackTile ? Color.black : Color.white;
    }

    public void InitPiece()
    {
        if(initialPiece == null) return;

        var piece = BoardManager.Instance.GeneratePiece(initialPiece);
        originalPiece = piece;
        SetPiece(piece);
    }

    public void SetPiece(Piece piece)
    {
        currentPiece = piece;
        currentPiece.SetTile(this);
    }

    private void Reset()
    {
        if(originalPiece == null) return;

        SetPiece(originalPiece); 
    }
}