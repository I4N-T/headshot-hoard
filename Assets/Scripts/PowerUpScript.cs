using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

    public GameObject playerObj;
    public AudioSource sfxSource;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FaceCamera();
    }

    void FaceCamera()
    {
        Vector3 targetPosition = new Vector3(playerObj.transform.position.x, transform.position.y, playerObj.transform.position.z);
        //transform.LookAt(targetPosition);
        transform.LookAt(targetPosition);
        transform.Rotate(0, 180, 0);
    }

    

}
