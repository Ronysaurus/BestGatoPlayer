using UnityEngine;
using UnityEngine.UI;

public class SCr_ButtonClass : MonoBehaviour
{
    public bool active, playerColor;
    public static GameObject[] buttons;
    public int index;

    private Color Cplayer, CAI;

    private void Start()
    {
        Cplayer = new Color(0.18f, 0.769f, 0.714f);
        CAI = new Color(0.906f, 0.114f, 0.212f);
        buttons = GameObject.FindGameObjectsWithTag("buttons");
        this.GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        if (SCR_GameManager.endGame)
            return;

        if (!SCR_GameManager.isPlayerTurn || active)
            return;
        active = true;
        playerColor = SCR_GameManager.isPlayerTurn;
        GetComponent<Image>().color = playerColor ? Cplayer : CAI;
        SCR_GameManager.boardMap[index / 3, index % 3] = playerColor ? 1 : 2;
        SCR_BestGatoPlayerLAN.lastButtonClicked = index;
        FindObjectOfType<SCR_GameManager>().NextTurn();
    }

    public void PickFromAI()
    {
        active = true;
        playerColor = SCR_GameManager.isPlayerTurn;
        GetComponent<Image>().color = playerColor ? Cplayer : CAI;
        SCR_GameManager.boardMap[index / 3, index % 3] = playerColor ? 1 : 2;
        SCR_BestGatoPlayerLAN.lastButtonClicked = index;
        FindObjectOfType<SCR_GameManager>().NextTurn();
    }

    public void ResetButton()
    {
        active = false;
        playerColor = false;
        GetComponent<Image>().color = Color.white;
    }
}