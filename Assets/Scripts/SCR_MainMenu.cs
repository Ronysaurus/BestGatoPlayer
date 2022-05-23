using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_MainMenu : MonoBehaviour
{
    public UIView main, credits, options;
    public UIPopup popup;

    public void Play()
    {
        SceneManager.LoadScene(1);
        popup.Hide();
    }

    public void Main()
    {
        options.Hide();
        credits.Hide();
        main.Show();
    }

    public void Credits()
    {
        main.Hide();
        credits.Show();
    }

    public void Options()
    {
        main.Hide();
        options.Show();
    }

    public void ConfirmReset()
    {
        PlayerPrefs.DeleteAll();
        popup.Hide();
    }

    public void CancelReset()
    {
        popup.Hide();
    }

    public void ResetAi()
    {
        popup.Show();
    }

    public void Quit()
    {
        Application.Quit();
    }
}