using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    private const int BoardSize = 8;

    [SerializeField] private float tileSize = 1f;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform tilesContent;
    [SerializeField] private Tile[] boardTiles;

    private float tileOffset => (BoardSize - 1) / 2f;
    private Transform piecesContent;

    public Tile[,] Board = new Tile[BoardSize,BoardSize];

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
        GetBoard();
        SetupPieces();
    }
    
    [ContextMenu("Create Board")]
    private void CreateBoard()
    {
        for(int x = 0; x < BoardSize; x++)
        {
            for(int y = 0; y < BoardSize; y++)
            {
                var tile = Instantiate(tilePrefab, tilesContent); 

                var pos = new Vector3((x - tileOffset) * tileSize, (y - tileOffset) * tileSize);
                var isBlackTile = (x + y) % 2 == 0;

                tile.Setup(this, new Vector2Int(x,y), pos, isBlackTile);
            }
        }
    }

    private void GetBoard()
    {
        for (int i = 0; i < boardTiles.Length; i++)
        {
            int x = i / BoardSize;
            int y = i % BoardSize;

            Board[x, y] = boardTiles[i];            
        }
    }

    private void SetupPieces()
    {
        piecesContent = new GameObject("Pieces").transform;
        piecesContent.transform.SetParent(transform);

        foreach (var tile in Board)
        {
            tile.InitPiece();
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
        return Board[coordinate.x, coordinate.y];
    }
}
