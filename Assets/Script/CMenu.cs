using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CMenu : MonoBehaviour {

    public CGameController gameController;

    public GameObject resultWin;
    public Text curStep;
    public Text bestStep;
    public GameObject resultLose;

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        gameController.StartGame();
        gameObject.SetActive(false);
    }

    public void Share()
    {

    }
}
