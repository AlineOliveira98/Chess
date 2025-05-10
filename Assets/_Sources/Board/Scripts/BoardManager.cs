using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    private const int BoardSize = 8;

    [SerializeField] private float tileSize = 1f;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Piece[] piecesPrefab;

    private float tileOffset => (BoardSize - 1) / 2f;
    private Transform piecesContent;
    private Transform tilesContent;
    private Dictionary<string, Piece> piecesDict = new();

    public Tile[,] board = new Tile[BoardSize,BoardSize];
    public Tile[,] Board => board;

    public static Action OnBoardReseted;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        InitPiecesDict();
        DrawBoard();
        DrawPieces();
    }

    private void InitPiecesDict()
    {
        piecesDict.Clear();

        for (int i = 0; i < piecesPrefab.Length; i++)
        {
            var key = $"{piecesPrefab[i].Type} {piecesPrefab[i].Color}";
            piecesDict.Add(key, piecesPrefab[i]);
        }
    }

    private Piece GetPiecePrefab(PieceType type, PieceColor color)
    {
        return piecesDict[$"{type} {color}"];
    }
    
    private void DrawBoard()
    {
        tilesContent = new GameObject("Tiles").transform;
        tilesContent.transform.SetParent(transform);
        
        for(int x = 0; x < BoardSize; x++)
        {
            for(int y = 0; y < BoardSize; y++)
            {
                var coord = new Vector2Int(x,y);
                var pos = new Vector3((x - tileOffset) * tileSize, (y - tileOffset) * tileSize);
                var tile = Instantiate(tilePrefab, tilesContent); 

                tile.transform.position = pos;
                tile.Setup(coord);

                board[x, y] = tile;
            }
        }
    }

    private void DrawPieces()
    {
        piecesContent = new GameObject("Pieces").transform;
        piecesContent.transform.SetParent(transform);

        for (int i = 0; i < 8; i++)
        {
            var whitePawn = GeneratePiece(GetPiecePrefab(PieceType.Pawn, PieceColor.White));
            var blackPawn = GeneratePiece(GetPiecePrefab(PieceType.Pawn, PieceColor.Black));

            board[i, 1].SetPiece(whitePawn);
            board[i, 6].SetPiece(blackPawn);
        }

        PieceType[] pieceTypesOrder = new PieceType[]
        {
            PieceType.Rook,
            PieceType.Knight,
            PieceType.Bishop,
            PieceType.Queen,
            PieceType.King,
            PieceType.Bishop,
            PieceType.Knight,
            PieceType.Rook
        };

        for (int i = 0; i < 8; i++)
        {
            var whitePiece = GeneratePiece(GetPiecePrefab(pieceTypesOrder[i], PieceColor.White));
            board[i, 0].SetPiece(whitePiece);

            var blackPiece = GeneratePiece(GetPiecePrefab(pieceTypesOrder[i], PieceColor.Black));
            board[i, 7].SetPiece(blackPiece);
        }
    }

    public Piece GeneratePiece(Piece piecePrefab)
    {
        var newPiece = Instantiate(piecePrefab, piecesContent.transform);

        return newPiece;
    }

    private void ResetBoard()
    {

    }

    public Tile GetTile(Vector2Int coordinate)
    {
        return board[coordinate.x, coordinate.y];
    }
}
