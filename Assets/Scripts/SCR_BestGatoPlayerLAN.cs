using LitJson;
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
    private List<int> banned;
    private int lastindex;
    private string lastBoardState = "";

    private void Start()
    {
        ideadText.text = "";
        options = new List<string>();
        randomOptions = new List<string>();
        banned = new List<int>();
        //Create data to save moves and "learn"
        if (!PlayerPrefs.HasKey("data"))
            PlayerPrefs.SetString("data", "{}");
        Debug.Log(PlayerPrefs.GetString("data"));
    }

    public void PlayTurn()
    {
        ideadText.text = "";
        boardState = "";
        //save the current state of the board to plan the next move
        for (int i = 0; i < 9; i++)
        {
            boardState += SCR_GameManager.boardMap[i / 3, i % 3].ToString();
        }
        Debug.Log(boardState);
        //choose the move to play and save it
        int buttonIndex = lastindex = ChooseAction();
        Debug.Log(buttonIndex);
        //show the selected move in game
        for (int i = 0; i < 9; i++)
        {
            if (buttonIndex == SCr_ButtonClass.buttons[i].GetComponent<SCr_ButtonClass>().index)
            {
                SCr_ButtonClass.buttons[i].GetComponent<SCr_ButtonClass>().PickFromAI();
            }
        }
    }

    private int ChooseAction()
    {
        options.Clear();
        banned.Clear();
        //get the saved data
        JsonData data = JsonMapper.ToObject(PlayerPrefs.GetString("data"));
        //if there's a state already saved uses that data
        if (data.ContainsKey(boardState))
        {
            Debug.Log("DejaVu");
            for (int i = 0; i < 9; i++)
            {
                if (data[boardState].ContainsKey(i.ToString()))
                {
                    if (int.Parse(data[boardState][i.ToString()].ToString()) == 1)
                    {
                        Debug.Log("I can win this");
                        return i;
                    }
                    if (int.Parse(data[boardState][i.ToString()].ToString()) == 0)
                        banned.Add(i);
                }
            }
        }
        //sees all passible options
        for (int i = 0; i < 9; i++)
        {
            if (SCR_GameManager.boardMap[i / 3, i % 3] == 0 && !banned.Contains(i))
                options.Add(i.ToString());
        }

        if (options.Count == 0)
            Surrender();
        else
            lastBoardState = boardState;

        //selects a random move from all viable options
        return int.Parse(options[Random.Range(0, options.Count)]);
    }

    public void DeleteBadOption()
    {
        //saves the last mistake to never do it again
        JsonData data = JsonMapper.ToObject(PlayerPrefs.GetString("data"));
        Debug.Log(lastindex);
        if (!data.ContainsKey(lastBoardState))
        {
            JsonData mistake = JsonMapper.ToObject("{}");
            mistake[lastindex.ToString()] = 0;
            data[lastBoardState] = mistake;
            PlayerPrefs.SetString("data", JsonMapper.ToJson(data));
            Debug.Log(PlayerPrefs.GetString("data"));
            data = JsonMapper.ToObject(PlayerPrefs.GetString("data"));
        }
        else
        {
            data[lastBoardState][lastindex.ToString()] = 0;
        }

        PlayerPrefs.SetString("data", JsonMapper.ToJson(data));
    }

    public void AddGoodOption()
    {
        //saves the last mistake to never do it again
        JsonData data = JsonMapper.ToObject(PlayerPrefs.GetString("data"));
        Debug.Log(lastindex);
        if (!data.ContainsKey(boardState))
        {
            JsonData win = JsonMapper.ToObject("{}");
            win[lastindex.ToString()] = 1;
            data[boardState] = win;
            PlayerPrefs.SetString("data", JsonMapper.ToJson(data));
            Debug.Log(PlayerPrefs.GetString("data"));
            data = JsonMapper.ToObject(PlayerPrefs.GetString("data"));
        }
        else
        {
            data[boardState][lastindex.ToString()] = 1;
        }

        PlayerPrefs.SetString("data", JsonMapper.ToJson(data));
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

        int choice = int.Parse(options[Random.Range(0, options.Count)]);
        for (int i = 0; i < 9; i++)
        {
            if (choice == SCr_ButtonClass.buttons[i].GetComponent<SCr_ButtonClass>().index)
            {
                SCr_ButtonClass.buttons[i].GetComponent<SCr_ButtonClass>().PickFromAI();
            }
        }
    }

    public void ResetAI()
    {
        PlayerPrefs.DeleteAll();
    }
}