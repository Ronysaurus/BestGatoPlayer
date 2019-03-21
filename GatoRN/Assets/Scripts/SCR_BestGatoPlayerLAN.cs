using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_BestGatoPlayerLAN : MonoBehaviour
{
    public static int lastButtonClicked;
    public Text ideadText;

    private string boardState = "";
    private List<string> options;
    private List<string> randomOptions;
    private List<string> banned;
    private int lastindex;
    private string lastBoardState = "";

    private void Start()
    {
        ideadText.text = "";
        options = new List<string>();
        randomOptions = new List<string>();
        banned = new List<string>();
    }

    public void PlayTurn()
    {
        ideadText.text = "";
        boardState = "";
        for (int i = 0; i < 9; i++)
        {
            boardState += SCR_GameManager.boardMap[i / 3, i % 3].ToString();
        }
        Debug.Log(boardState);
        int buttonIndex = lastindex = ChooseAction();
        Debug.Log(buttonIndex);
        SCr_ButtonClass.buttons[buttonIndex].GetComponent<SCr_ButtonClass>().PickFromAI();
    }

    private int ChooseAction()
    {
        options.Clear();
        banned.Clear();
        if (PlayerPrefs.HasKey(boardState))
        {
            Debug.Log("DejaVu");
            string result = PlayerPrefs.GetString(boardState);
            for (int i = 0; i < result.Split(',').Length; i++)
                banned.Add(result.Split(',')[i]);
        }
        for (int i = 0; i < 9; i++)
        {
            if (SCR_GameManager.boardMap[i / 3, i % 3] == 0 && !banned.Contains(i.ToString()))
                options.Add(i.ToString());
        }

        if (options.Count == 0)
            Surrender();
        else
            lastBoardState = boardState;

        return int.Parse(options[Random.Range(0, options.Count)]);
    }

    public void DeleteBadOption()
    {
        string past = "";
        if (PlayerPrefs.HasKey(lastBoardState))
        {
            past = PlayerPrefs.GetString(lastBoardState);
            Debug.Log("new mistake");
        }
        Debug.Log(lastindex);
        PlayerPrefs.SetString(lastBoardState, past + lastindex.ToString() + ",");
        Debug.Log("boardState: " + lastBoardState + " mistakes: " + past + lastindex.ToString());
    }

    public void Surrender()
    {
        FindObjectOfType<SCR_GameManager>().PlayerWin();
        ideadText.text = "I can't win this";
        Debug.Log("I can't win this");
    }

     public void Randomchoose()
    {
        for (int i = 0; i < 9; i++)
        {
            if (SCR_GameManager.boardMap[i / 3, i % 3] == 0)
                randomOptions.Add(i.ToString());
        }
        SCr_ButtonClass.buttons[int.Parse(options[Random.Range(0, options.Count)])].GetComponent<SCr_ButtonClass>().PickFromAI();
    }

    public void ResetAI()
    {
        PlayerPrefs.DeleteAll();
    }
}