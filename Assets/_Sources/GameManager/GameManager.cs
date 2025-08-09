using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PieceColor playersColor;

    public PieceColor PlayersColor => playersColor;

    public static Action<PieceColor> OnTurnChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ChangeTurn()
    {
        playersColor = playersColor == PieceColor.White ? PieceColor.Black : PieceColor.White;

        OnTurnChanged?.Invoke(playersColor);
    }
}
