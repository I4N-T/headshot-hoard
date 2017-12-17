using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleManager : MonoBehaviour {

    public Button playBtn;
    public Button quitBtn;
    public AudioSource sfxSource;
    public AudioClip headShotClip;

    void Start()
    {
        playBtn.onClick.AddListener(PlayButtonMethod);
        quitBtn.onClick.AddListener(QuitButtonMethod);
    }

    void PlayButtonMethod()
    {
        sfxSource.clip = headShotClip;
        sfxSource.Play();
        StartCoroutine(LoadLevelAfterDelay(1.5f));
    }

    void QuitButtonMethod()
    {
        Application.Quit();
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }


}
