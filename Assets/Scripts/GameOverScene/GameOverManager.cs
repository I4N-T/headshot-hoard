using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    GameObject gameManagerObj;

    public Text scoreTxt;
    public Button replayBtn;
    public Button quitBtn;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        gameManagerObj = GameObject.FindGameObjectWithTag("GameController");

        print("Final Score: " + GameManager.score.ToString());
        scoreTxt.text = "Final Score: " + GameManager.score.ToString();
        Destroy(gameManagerObj);

        replayBtn.onClick.AddListener(ReplayButtonMethod);
        quitBtn.onClick.AddListener(QuitButtonMethod);
    }

    void ReplayButtonMethod()
    {
        
        SceneManager.LoadScene(1);
    }

    void QuitButtonMethod()
    {
        Application.Quit();
    }
}
