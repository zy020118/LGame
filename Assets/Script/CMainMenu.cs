using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CMainMenu : MonoBehaviour {

    public CGameController game;

    public void SelectGame(int id)
    {
        SceneManager.LoadScene(id);
    }
}
