using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    //REFERENCES TO OTHER STUFF
    public GameObject playerObj;
    public GameObject powerUpObj;

    //REFERENCES TO OWN STUFF
    public GameObject head;

    //STATS
    public float health;
    public int chance;

    //OTHER
    bool isPlayerSeen;
    bool isDamaging;
    bool isCRStarted;
    public static bool isShuttingDown;

    //AUDIO
    public AudioSource sfxSource;
    public AudioClip playerHitSound;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        head = gameObject.transform.GetChild(0).gameObject;

        health = 5f;
    }

    void Update()
    {
        FaceCamera();
        checkForDeath();
        LookForPlayer();

        //Restart damage cr if need to
        if (isDamaging)
        {
            if (!isCRStarted)
            {
                StartCoroutine(DamageOverTime());
                isCRStarted = true;
            }
        }

        if (isPlayerSeen)
        {
            GoGetPlayer();
        }
    }

    void FaceCamera()
    {
        Vector3 targetPosition = new Vector3(playerObj.transform.position.x, transform.position.y
            , playerObj.transform.position.z);
        transform.LookAt(targetPosition);
    }

    void checkForDeath()
    {
        if (health <= 0)
        {
            //die
            GameManager.score += 1;
            Destroy(gameObject);
        }
    }

    void LookForPlayer()
    {
        //if raycast hits AND within range then go after player
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                isPlayerSeen = true;
            }
        }
    }

    void GoGetPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerObj.transform.position);
        if (distance <= 100f)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerObj.transform.position.x, 1.5f, playerObj.transform.position.z), 3f * Time.deltaTime);
    }

    IEnumerator DamageOverTime()
    {
        PlayerController playerScript = playerObj.GetComponent<PlayerController>();
        while (isDamaging)
        {
            PlayerController.playerHealth -= 5;
            //grunt
            sfxSource.clip = playerHitSound;
            sfxSource.Play();
            yield return new WaitForSeconds(1f);
        }
        isCRStarted = false;

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isDamaging = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            isDamaging = false;
            //stop coroutine?
        }
    }


    void OnDestroy()
    {
        int rand = Random.Range(0, chance);
        if (rand == 1)
        {
            //possible problem here when changing scene
            SafeInstantiate(powerUpObj, new Vector3(transform.position.x, .5f, transform.position.z), Quaternion.identity);
        }     
    }

    public GameObject SafeInstantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (isShuttingDown)
        {
            return null;
        }
        else 
        {
            return Instantiate(prefab, position, rotation) as GameObject;
        }
    }
    void OnApplicationQuit()
    {
        isShuttingDown = true; ;
    }

}
