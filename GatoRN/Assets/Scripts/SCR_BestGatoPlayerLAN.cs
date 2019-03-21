using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SCR_BestGatoPlayerLAN : MonoBehaviour
{
    public int lastButtonClicked;

    private string boardState = "";
    private string path = "";
    private SCr_ButtonClass[] buttons;
    private List<string> options;
    private int lastindex;
    private string lastPath;

    private void Start()
    {
        options = new List<string>();
        path = "Resources";
        buttons = FindObjectsOfType<SCr_ButtonClass>();
    }

    public void PlayTurn()
    {
        path += "/" + lastButtonClicked.ToString();
        boardState = "";
        for (int i = 0; i < 9; i++)
        {
            boardState += SCR_GameManager.boardMap[i].ToString();
        }
        Debug.Log(path);
        Debug.Log(boardState);
        int buttonIndex = ChooseAction();
        Debug.Log(buttonIndex);
        buttons[buttonIndex].PickFromAI();
        path += "/" + buttonIndex.ToString();
    }

    private int ChooseAction()
    {
        options.Clear();
        StreamReader reader = new StreamReader(lastPath = Application.dataPath + "/" + path + "/options.txt");
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line != "deleted")
            {
                options.Add(line);
            }
        }
        lastindex = Random.Range(0, options.Count);
        return int.Parse(options[lastindex]);
    }

    public void DeleteBadOption()
    {
        //string [] lines =  File.ReadAllLines(lastPath);
        //lines[lastindex] = "deleted";
        //File.WriteAllLines(lastPath, lines);
    }

    public void RewardGoodOption()
    {
        //add good option to the end of file
    }
}