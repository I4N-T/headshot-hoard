using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text scoreTxt;
    public Text healthTxt;

    private void Update()
    {
        if (scoreTxt != null && healthTxt != null)
        {
            scoreTxt.text = GameManager.score.ToString();
            healthTxt.text = PlayerController.playerHealth.ToString();
        }
        
    }
}
