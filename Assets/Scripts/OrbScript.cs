using UnityEngine;
using System.Collections;

public class OrbScript : MonoBehaviour {

    public Vector3 trajectory;
    public AudioSource sfxSource;
    public AudioClip playerHitSound;

    void Start()
    {
        sfxSource = gameObject.transform.GetComponent<AudioSource>();
        sfxSource.clip = playerHitSound;
    }

    void Update()
    {
        transform.Translate(trajectory * Time.deltaTime * 8f);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Zombie" || col.tag != "ZombieTeleport")
        {
            if (col.tag == "Player")
            {
                //PlayerController playerScript = col.gameObject.GetComponent<PlayerController>();
                PlayerController.playerHealth -= 20;
                sfxSource.Play();
                //print("test ok");
            }
            Destroy(gameObject, 0.5f);

        }


    }
}
