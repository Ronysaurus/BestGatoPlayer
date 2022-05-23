using UnityEngine;

public class SCR_GameManager : MonoBehaviour
{
    public static bool isPlayerTurn;
    public static int turnNum;

    public static int[,] boardMap;

    public static bool endGame;

    private static SCR_BestGatoPlayerLAN IA;

    public TMPro.TextMeshProUGUI playerWinsText, AIWinsText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("player"))
        {
            PlayerPrefs.SetInt("player", 0);
            PlayerPrefs.SetInt("ai", 0);
        }

        playerWinsText.text = PlayerPrefs.GetInt("player").ToString();
        AIWinsText.text = PlayerPrefs.GetInt("ai").ToString();

        boardMap = new int[3, 3];

        IA = FindObjectOfType<SCR_BestGatoPlayerLAN>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardMap[i, j] = 0;
            }
        }

        isPlayerTurn = true;
        turnNum = 1;
    }

    public void NextTurn()
    {
        turnNum++;
        isPlayerTurn = !isPlayerTurn;
        if (CheckForWin())
        {
            endGame = true;
            if (isPlayerTurn)
                IAWin();
            else
                PlayerWin();
        }
        if (endGame)
            return;

        if (!isPlayerTurn)
            IA.PlayTurn();
    }

    private void IAWin()
    {
        endGame = true;
        PlayerPrefs.SetInt("ai", PlayerPrefs.GetInt("ai") + 1);
        AIWinsText.text = PlayerPrefs.GetInt("ai").ToString();
        IA.AddGoodOption();
        Debug.Log("IA WINS");
    }

    public void PlayerWin()
    {
        endGame = true;
        PlayerPrefs.SetInt("player", PlayerPrefs.GetInt("player") + 1);
        playerWinsText.text = PlayerPrefs.GetInt("player").ToString();
        Debug.Log("Player Wins");
        IA.DeleteBadOption();
    }

    private static void Draw()
    {
        Debug.Log("Draw");
    }

    private static bool CheckForWin()
    {
        int s = isPlayerTurn ? 2 : 1;
        const int n = 3;
        endGame = true;
        bool win = false;
        int lastPlayColumn = SCR_BestGatoPlayerLAN.lastButtonClicked / 3;
        int lastPlayRow = SCR_BestGatoPlayerLAN.lastButtonClicked % 3;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boardMap[i, j] == 0)
                {
                    endGame = false;
                    break;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (boardMap[lastPlayColumn, i] != s)
                break;
            if (i == n - 1)
            {
                win = true;
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (boardMap[i, lastPlayRow] != s)
                break;
            if (i == n - 1)
            {
                win = true;
            }
        }

        if (lastPlayRow == lastPlayColumn)
        {
            for (int i = 0; i < n; i++)
            {
                if (boardMap[i, i] != s)
                    break;
                if (i == n - 1)
                {
                    win = true;
                }
            }
        }

        if (lastPlayColumn + lastPlayRow == n - 1)
        {
            for (int i = 0; i < n; i++)
            {
                if (boardMap[i, n - 1 - i] != s)
                    break;
                if (i == n - 1)
                {
                    win = true;
                }
            }
        }

        if (!win && endGame)
            Draw();

        return win;
    }

    public void ResetBoard()
    {
        endGame = false;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                SCr_ButtonClass.buttons[(i * 3) + j].GetComponent<SCr_ButtonClass>().ResetButton();
                boardMap[i, j] = 0;
            }
        }

        isPlayerTurn = true;
        turnNum = 1;
    }
}