using UnityEngine;

public class SCR_GameManager : MonoBehaviour
{
    public static bool isPlayerTurn;
    public static int turnNum;
    public static int[] boardMap = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private static SCR_BestGatoPlayerLAN IA;

    private void Start()
    {
        IA = FindObjectOfType<SCR_BestGatoPlayerLAN>();
        for (int i = 0; i < 9; i++)
        {
            boardMap[i] = 0;
        }

        isPlayerTurn = true;
        turnNum = 1;
    }

    public static void NextTurn()
    {
        turnNum++;
        isPlayerTurn = !isPlayerTurn;
        if (turnNum > 6 && CheckForWin())
        {
            //endgame
        }
        if (!isPlayerTurn)
            IA.PlayTurn();
    }

    private static bool CheckForWin()
    {
        return false;
    }
}