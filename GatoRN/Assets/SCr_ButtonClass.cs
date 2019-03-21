using UnityEngine;
using UnityEngine.UI;

public class SCr_ButtonClass : MonoBehaviour
{
    public bool active, playerColor;
    public static GameObject[] buttons;

    private void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("buttons");
    }

    public void OnButtonClicked()
    {
        if (SCR_GameManager.endGame)
            return;

        if (!SCR_GameManager.isPlayerTurn || active)
            return;
        active = true;
        playerColor = SCR_GameManager.isPlayerTurn;
        GetComponent<Image>().color = playerColor ? Color.blue : Color.red;
        for (int i = 0; i < 9; i++)
        {
            if (buttons[i] == this.gameObject)
            {
                SCR_GameManager.boardMap[i / 3 , i % 3] = playerColor ? 1 : 2;
                SCR_BestGatoPlayerLAN.lastButtonClicked = i;
            }
        }
        FindObjectOfType<SCR_GameManager>().NextTurn();
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
                SCR_GameManager.boardMap[i / 3 , i % 3] = playerColor ? 1 : 2;
                SCR_BestGatoPlayerLAN.lastButtonClicked = i;
            }
        }
        FindObjectOfType<SCR_GameManager>().NextTurn();
    }

    public void ResetButton()
    {
        active = false;
        playerColor = false;
        GetComponent<Image>().color = Color.white;
    }
}