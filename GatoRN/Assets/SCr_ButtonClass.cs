using UnityEngine;
using UnityEngine.UI;

public class SCr_ButtonClass : MonoBehaviour
{
    public bool active, playerColor;
    private GameObject[] buttons;
    SCR_BestGatoPlayerLAN gatoPlayerLAN;

    private void Start()
    {
        gatoPlayerLAN = FindObjectOfType<SCR_BestGatoPlayerLAN>();
        buttons = GameObject.FindGameObjectsWithTag("buttons");
    }

    public void OnButtonClicked()
    {
        if (!SCR_GameManager.isPlayerTurn || active)
            return;
        active = true;
        playerColor = SCR_GameManager.isPlayerTurn;
        GetComponent<Image>().color = playerColor ? Color.blue : Color.red;
        for (int i = 0; i < 9; i++)
        {
            if (buttons[i] == this.gameObject)
            {
                SCR_GameManager.boardMap[i] = playerColor ? 1 : 2;
                gatoPlayerLAN.lastButtonClicked = i;
            }
        }
        SCR_GameManager.NextTurn();
    }

    public void PickFromAI()
    {
        active = true;
        playerColor = SCR_GameManager.isPlayerTurn;
        GetComponent<Image>().color = playerColor ? Color.blue : Color.red;
        for (int i = 0; i < 9; i++)
        {
            if (buttons[i] == this.gameObject)
            {
                SCR_GameManager.boardMap[i] = playerColor ? 1 : 2;
                gatoPlayerLAN.lastButtonClicked = i;
            }
        }
        SCR_GameManager.NextTurn();
    }
}