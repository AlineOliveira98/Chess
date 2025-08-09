using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTurn;

    void Start()
    {
        UpdatePlayerTurnText(GameManager.Instance.PlayersColor);
    }

    void OnEnable()
    {
        GameManager.OnTurnChanged += UpdatePlayerTurnText;
    }

    void OnDisable()
    {
        GameManager.OnTurnChanged -= UpdatePlayerTurnText;
    }

    private void UpdatePlayerTurnText(PieceColor pieceColor)
    {
        playerTurn.text = $"Current turn\r\n{pieceColor}";
    }

    public void ResetBoard()
    {
        BoardController.Instance.ResetBoard();
    }
}
