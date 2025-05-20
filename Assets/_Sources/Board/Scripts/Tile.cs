using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private GameObject highlight;

    private Piece initialPiece;
    private Piece currentPiece;
    
    public Vector2Int Coord {get; private set; }

    public Vector2 Position => transform.position;
    public bool IsEmpty => currentPiece == null;
    public Piece CurrentPiece => currentPiece;

    void OnEnable()
    {
        BoardController.OnBoardReseted += Reset; 
    }

    void OnDisable()
    {
        BoardController.OnBoardReseted -= Reset;
    }

    void Start()
    {
        
    }

    public void Setup(Vector2Int coord)
    {
        Coord = coord;
        gameObject.name = $"Tile {coord.x}-{coord.y}";

        var tileBlackColor = (coord.x + coord.y) % 2 == 0;

        var color = tileBlackColor ? Color.black : Color.white;
        background.color = color;
    }

    public void SetInitialPiece(Piece piece)
    {
        initialPiece = piece;
    }

    public void SetPiece(Piece piece)
    {
        currentPiece = piece;
        currentPiece.SetTile(this);
    }

    public void SetHighLight(bool isHighlighted = false)
    {
        highlight.SetActive(isHighlighted);
    }

    public void CapturePiece()
    {
        if(currentPiece == null) return;

        currentPiece.gameObject.SetActive(false);
        RemovePiece();
    }

    public void RemovePiece()
    {
        currentPiece = null;
    }

    private void Reset()
    {
        if(initialPiece != null) 
            SetPiece(initialPiece);
        else
            currentPiece = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsEmpty) return;

        BoardController.Instance.MovePiece(this);
    }
}