using UnityEngine;
using System.Collections;

public class ZombieTeleportScript : MonoBehaviour {

    //REFERENCES TO OTHER STUFF
    public GameObject playerObj;
    public GameObject powerUpObj;
    public GameObject orbObj;
    public GameObject orbThrowObj;

    //REFERENCES TO OWN STUFF
    public GameObject head;

    //STATS
    public float health;
    public int chance;

    //OTHER
    bool isPlayerSeen;
    bool isDamaging;
    bool isCRStarted;
    //bool isShuttingDown;

    public Vector3 trajectory;
    Vector3 targetTele;

    //AUDIO

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        head = gameObject.transform.GetChild(0).gameObject;

        StartCoroutine(Teleport());

        health = 5f;
    }

    void Update()
    {
        FaceCamera();
        checkForDeath();
        LookForPlayer();

        //Update target position
        gameObject.transform.position = targetTele;

        //Restart damage cr if need to
        if (isDamaging)
        {
            if (!isCRStarted)
            {

                isCRStarted = true;
            }
        }

        if (isPlayerSeen)
        {
            if (!isCRStarted)
            {
                StartCoroutine(ThrowOrb());
            }
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
        if (Physics.Raycast(gameObject.transform.position, (playerObj.transform.position - gameObject.transform.position).normalized, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                isPlayerSeen = true;
            }
        }
    }

    IEnumerator ThrowOrb()
    {
        while (isPlayerSeen)
        {

            trajectory = (playerObj.transform.position - gameObject.transform.position).normalized;
            isCRStarted = true;
            orbThrowObj = Instantiate(orbObj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            OrbScript orbScript = orbThrowObj.GetComponent<OrbScript>();
            orbScript.trajectory = trajectory;
            yield return new WaitForSeconds(1.5f);
        }

        isCRStarted = false;

    }

    IEnumerator Teleport()
    {
        while (true)
        {

            int locationChoice = Random.Range(0, 4);
            //killing fields
            if (locationChoice == 0)
            {
                targetTele = new Vector3(Random.Range(-17, 17), 4, Random.Range(-17, 17));
            }
            //tunnel
            else if (locationChoice == 1)
            {
                int tunnelChoice = Random.Range(0, 7);
                if (tunnelChoice == 0)
                {
                    targetTele = new Vector3(Random.Range(-2, 2), 4, Random.Range(18, 37));
                }
                else if (tunnelChoice == 1)
                {
                    targetTele = new Vector3(Random.Range(8, 17), 4, Random.Range(43, 47));
                }
                else if (tunnelChoice == 2)
                {
                    targetTele = new Vector3(Random.Range(28, 32), 4, Random.Range(-19.5f, 29.5f));
                }
                else if (tunnelChoice == 3)
                {
                    targetTele = new Vector3(Random.Range(18, 37), 4, Random.Range(-2, 2));
                }
                else if (tunnelChoice == 4)
                {
                    targetTele = new Vector3(Random.Range(-2, 2), 4, Random.Range(-32, -18));
                }
                else if (tunnelChoice == 5)
                {
                    targetTele = new Vector3(Random.Range(2.5f, 17), 4, Random.Range(-32, -28));
                }
                else if (tunnelChoice == 6)
                {
                    targetTele = new Vector3(Random.Range(47, 52), 4, Random.Range(-34.5f, -10.5f));
                }
            }
            //small rooms
            else if (locationChoice == 2)
            {
                int quadChoice = Random.Range(0, 3);
                if (quadChoice == 0)
                {
                    targetTele = new Vector3(Random.Range(-7f, 7f), 4, Random.Range(36.5f, 52f));
                }
                else if (quadChoice == 1)
                {
                    targetTele = new Vector3(Random.Range(18f, 32), 4, Random.Range(30.5f, 59.5f));
                }
                else if (quadChoice == 2)
                {
                    targetTele = new Vector3(Random.Range(38f, 52f), 4, Random.Range(-9.5f, 9.5f));
                }

            }
            //cave room
            if (locationChoice == 3)
            {
                targetTele = new Vector3(Random.Range(18, 42), 4, Random.Range(-44.5f, -19.5f));
            }

            yield return new WaitForSeconds(4f);
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
        if (ZombieScript.isShuttingDown)
        {
            return null;
        }
        else
        {
            return Instantiate(prefab, position, rotation) as GameObject;
        }
    }
    /*void OnApplicationQuit()
    {
        isShuttingDown = true; ;
    }*/

}
